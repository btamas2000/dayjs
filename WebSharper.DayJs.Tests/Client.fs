namespace WebSharper.DayJs.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.DayJs

[<JavaScript>]
module Client =

    type IndexTemplate = Template<"index.html", ClientLoad.FromDocument>

    [<SPAEntryPoint>]
    let Main () =

        let someDate = DayJs("2019-01-25").ToString()
        let formattedDate = DayJs("12-25-1995", "assdfsdfsdf").ToString()
        let localeTest1 = DayJs().LocalLocale(Locale.JA)
        let localeTest2 = DayJs().LocalLocale(Locale.HU)
        let notUTCDayJs = DayJs().IsUTC()
        let UTCDayJs = DayJs().Utc().IsUTC()
        let tz1 = DayJs().Tz("Europe/Budapest")
        let tz2 = DayJs().Tz("America/New_York")
        let guess = DayJs.Guess()
        let dur1 = DayJs.Duration(1, DurationUnits.Minutes)
        let dur2 = DayJs.Duration(2, DurationUnits.Minutes)
        let dur3 = DayJs.Duration(24, DurationUnits.Hours)

        IndexTemplate.Main()
            .DatesAsString(
                Doc.Concat [
                    h2 [] [text "Plugin test (using CustomParseFormat)"]
                    p [] [text someDate]
                    p [] [text ("This should be invalid: " + formattedDate)]
                ]
            )
            .ObjectSupp(
                let fromObj = FromStandardObject()
                fromObj.Years <- 2010
                fromObj.Months <- 3
                fromObj.Date <- 5
                fromObj.Hours <- 15
                fromObj.Minutes <- 10
                fromObj.Seconds <- 3
                fromObj.Milliseconds <- 123
                Doc.Concat [
                    h2 [] [text "ObjectSupport test"]
                    p [] [text ((DayJs(fromObj)).ToString())]
                ]
            )
            .LocaleTest(
                Doc.Concat [
                    h2 [] [text "Locale test"]
                    p [] [text (localeTest1.Format("YYYY MMM DD"))]
                    p [] [text (localeTest2.Format("YYYY MMM DD"))]
                ]
            )
            .UTCTest(
                Doc.Concat [
                    h2 [] [text "UTC test"]
                    p [] [text (notUTCDayJs.ToString())]
                    p [] [text (UTCDayJs.ToString())]
                ]
            )
            .TimeZoneTest(
                Doc.Concat [
                    h2 [] [text "Timezone test"]
                    p [] [text (tz1.Format())]
                    p [] [text (tz2.Format())]
                    p [] [text guess]
                ]
            )
            .DurationTest(
                Doc.Concat [
                    h2 [] [text "Duration test"]
                    p [] [text (dur1.Humanize())]
                    p [] [text (dur2.Humanize())]
                    p [] [text (dur3.Humanize())]
                ]
            )
            .Doc()
        |> Doc.RunById "main"

