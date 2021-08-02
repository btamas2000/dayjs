namespace WebSharper.DayJs.Extension

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =

    let FromStandardObject =
        Pattern.Config "FromStandardObject" {
            Required = []
            Optional = [
                "year", T<int>
                "month", T<int>
                "day", T<int>
                "hour", T<int>
                "minute", T<int>
                "second", T<int>
                "millisecond", T<int>
            ]
        }

    let FromStandardPluralObject =
        Pattern.Config "FromStandardPluralObject" {
            Required = []
            Optional = [
                "years", T<int>
                "months", T<int>
                "date", T<int>
                "hours", T<int>
                "minutes", T<int>
                "seconds", T<int>
                "milliseconds", T<int>
            ]
        }

    let DayJs =
        Class "dayjs"
        |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.MainResource>]
        |> WithSourceName "DayJs"
        |+> Static [
            Constructor T<unit>
            Constructor T<Date>
            Constructor T<string>
            Constructor (T<string> * (T<string> + !| T<string>) * !? T<bool>)
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.CustomParseFormatResource>]
            Constructor (T<string> * (T<string> + !| T<string>) * T<string> * !? T<bool>)
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.CustomParseFormatResource>]
            Constructor (T<int>)
            Constructor (FromStandardObject + FromStandardPluralObject)
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ObjectSupport>]

            "extend" => T<obj> ^-> T<unit>

            "unix" => (T<int> + T<float>) ^-> TSelf
        ]
        |+> Instance [
            "toString" => T<unit> ^-> T<string>
        ]

    let Assembly =
        Assembly [
            // Namespace "WebSharper.DayJs.Resources" [
            //     // DayJsResources.CustomParseFormatResource
                
            // ]
            Namespace "WebSharper.DayJs" [
                FromStandardObject
                FromStandardPluralObject
                DayJs
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
