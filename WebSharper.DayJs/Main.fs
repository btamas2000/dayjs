namespace WebSharper.DayJs.Extension

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =

    let FromStandardObject =
        Pattern.Config "FromStandardObject" {
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

    let GetSetUnits =
        Pattern.EnumStrings "GetSetUnits" [
            "date"
            "day"
            "month"
            "year"
            "hour"
            "minute"
            "second"
            "millisecond"
        ]

    let AddSubtractUnits =
        Pattern.EnumStrings "AddSubtractUnits" [
            "day"
            "week"
            "month"
            "quarter"
            "year"
            "hour"
            "minute"
            "second"
            "millisecond"
        ]

    let StartOfUnits =
        Pattern.EnumStrings "StartOfUnits" [
            "day"
            "date"
            "week"
            "isoWeek"
            "month"
            "quarter"
            "year"
            "hour"
            "minute"
            "second"
        ]

    let UTC =
        Class "dayjs.utc"
        |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.UTCResource>]
        |> WithSourceName "UTC"
        |+> Static [
            Constructor T<unit>
            Constructor T<bool>
        ]
        |+> Instance [
            "local" => T<unit> ^-> TSelf
        ]

    let Inclusivity =
        Pattern.EnumInlines "Inclusivity" [
            "InclIncl", "'[]'"
            "InclExl", "'[)'"
            "ExlIncl", "'(]'"
            "ExlExl", "'()'"
        ]

    let StaticLocaleData =
        Class "dayjs.localeDate"
        |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
        |> WithSourceName "StaticLocaleData"
        |+> Static [
            Constructor T<unit>
        ]
        |+> Instance [
            "firstDayOfWeek" => T<unit> ^-> !| T<string>
            "months" => !? TSelf ^-> !| T<string>
            "monthsShort" => !? TSelf ^-> !| T<string>
            "weekdays" => !? TSelf ^-> !| T<string>
            "weekdaysShort" => !? TSelf ^-> !| T<string>
            "weekdaysMin" => !? TSelf ^-> !| T<string>
        ]

    let InstanceLocaleData =
        Class "dayjs().localeDate"
        |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
        |> WithSourceName "InstanceLocaleData"
        |+> Static [
            Constructor T<unit>
        ]
        |+> Instance [
            "firstDayOfWeek" => T<unit> ^-> !| T<string>
            "months" => T<unit> ^-> !| T<string>
            "monthsShort" => T<unit> ^-> !| T<string>
            "weekdays" => T<unit> ^-> !| T<string>
            "weekdaysShort" => T<unit> ^-> !| T<string>
            "weekdaysMin" => T<unit> ^-> !| T<string>
        ]

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
            Constructor FromStandardObject
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ObjectSupportResource>]
            Constructor (!| T<int>)
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ArraySupportResource>]    

            "clone" => T<unit> ^-> TSelf
            "unix" => (T<int> + T<float>) ^-> TSelf
            "min" => !| TSelf ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.MinMaxResource>]
            "max" => !| TSelf ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.MinMaxResource>]
            "isDayjs" => T<obj> ^-> T<bool>
            "locale" => T<unit> ^-> T<string>
            "locale" => T<string> ^-> TSelf // TODO
            "localeData" => T<unit> ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "firstDayOfWeek" => T<unit> ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "months" => !? TSelf ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "monthsShort" => !? TSelf ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "weekdays" => !? TSelf ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "weekdaysShort" => !? TSelf ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "weekdaysMin" => !? TSelf ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "tz" => T<string> * T<string> ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.TimeZoneResource>]
            "tz" => T<string> * T<string> * T<string> ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.TimeZoneResource>]
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.CustomParseFormatResource>]
        ]
        |+> Instance [
            "isValid" => T<unit> ^-> T<bool>
            "valueOf" => T<unit> ^-> T<int>
            // get set
            "millisecond" => T<unit> ^-> T<int>
            "millisecond" => T<int> ^-> T<unit>
            "second" => T<unit> ^-> T<int>
            "second" => T<int> ^-> T<unit>
            "minute" => T<unit> ^-> T<int>
            "minute" => T<int> ^-> T<unit>
            "hour" => T<unit> ^-> T<int>
            "hour" => T<int> ^-> T<unit>
            "date" => T<unit> ^-> T<int>
            "date" => T<int> ^-> T<unit>
            "day" => T<unit> ^-> T<int>
            "day" => T<int> ^-> T<unit>
            "weekday" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.WeekdayResource>]
            "weekday" => T<int> ^-> T<unit>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.WeekdayResource>]
            "isoWeekday" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ISOWeekdayResource>]
            "isoWeekday" => T<int> ^-> T<unit>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ISOWeekdayResource>]
            "dayOfYear" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.DayOfYearResource>]
            "dayOfYear" => T<int> ^-> T<unit>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.DayOfYearResource>] 
            "week" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.WeekOfYearResource>]
            "week" => T<int> ^-> T<unit>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.WeekOfYearResource>]
            "isoWeek" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ISOWeekResource>]
            "isoWeek" => T<int> ^-> T<unit>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ISOWeekResource>]
            "month" => T<unit> ^-> T<int>
            "month" => T<int> ^-> T<unit>
            "quarter" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.QuarterOfYearResource>]
            "quarter" => T<int> ^-> T<unit>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.QuarterOfYearResource>]
            "year" => T<unit> ^-> T<int>
            "year" => T<int> ^-> T<unit>
            "weekYear" => T<unit> ^-> T<int>
            "isoWeekYear" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ISOWeekResource>]
            "isoWeeksInYear" => T<unit> ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ISOWeeksInYearResource>]
            "get" => GetSetUnits ^-> T<string>
            "set" => GetSetUnits * T<int> ^-> T<string>
            // manipulate
            "add" => T<int> * AddSubtractUnits ^-> TSelf
            "add" => FromStandardObject ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.DurationResource>]
            "subtract" => T<int> * AddSubtractUnits ^-> TSelf
            "startOf" => StartOfUnits ^-> TSelf
            "endOf" => StartOfUnits ^-> TSelf
            "utcOffset" => T<int> * !? T<bool> ^-> T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.UTCResource>]
            // display
            "format" => !? T<string> ^-> T<string>
            //localizedformat TODO
            "fromNow" => !? T<bool> ^-> T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.RelativeTimeResource>]
            "from" => TSelf * !? T<bool> ^-> T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.RelativeTimeResource>]
            "toNow" => !? T<bool> ^-> T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.RelativeTimeResource>]
            "to" => TSelf * !? T<bool> ^-> T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.RelativeTimeResource>]
            "calendar" => !? TSelf * !? T<obj> ^-> T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.CalendarResource>]
            "diff" => TSelf * !? AddSubtractUnits ^-> (T<int> + T<float>)
            "diff" => TSelf * AddSubtractUnits * T<bool> ^-> (T<int> + T<float>)
            "unix" => T<unit> ^-> T<int>
            "daysInMonth" => T<unit> ^-> T<int>
            "toDate" => T<unit> ^-> T<Date>
            "toArray" => T<unit> ^-> !| T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ToArrayResource>]
            "toJSON" => T<unit> ^-> T<string>
            "toISOString" => T<unit> ^-> T<string>
            "toObject" => T<unit> ^-> T<obj>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.ToObjectResource>]
            "toString" => T<unit> ^-> T<string>
            // query
            "isBefore" => TSelf * !? StartOfUnits ^-> T<int>
            "isSame" => TSelf * !? StartOfUnits ^-> T<int>
            "isAfter" => TSelf * !? StartOfUnits ^-> T<int>
            "isSameOrBefore" => TSelf * !? StartOfUnits ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.IsSameOrBeforeResource>]
            "isSameOrAfter" => TSelf * !? StartOfUnits ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.IsSameOrAfterResource>]
            "isBetween" => TSelf * TSelf * !? StartOfUnits * !? Inclusivity ^-> T<int>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.IsBetweenResource>]
            "isLeapYear" => T<unit> ^-> T<bool>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.IsLeapYearResource>]
            // i18n
            "locale" => T<string> ^-> TSelf // TODO
            "localeData" => T<unit> ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "firstDayOfWeek" => T<unit> ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "months" => T<unit> ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "monthsShort" => T<unit> ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "weekdays" => T<unit> ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "weekdaysShort" => T<unit> ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            "weekdaysMin" => T<unit> ^-> !| T<string>
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.LocaleDataResource>]
            // plugins
            // customize
            // durations
            // timezone
            "tz" => T<string> * !? T<bool> ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.TimeZoneResource>]
            "guess" => T<unit> ^-> T<string>
            |> WithInline "tz.guess()"
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.TimeZoneResource>]
            "tz.setDefault" => !? T<string> ^-> TSelf
            |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.TimeZoneResource>]
        ]

    let Assembly =
        Assembly [
            // Namespace "WebSharper.DayJs.Resources" [
            //     // DayJsResources.CustomParseFormatResource
                
            // ]
            Namespace "WebSharper.DayJs" [
                FromStandardObject
                GetSetUnits
                AddSubtractUnits
                StartOfUnits
                UTC
                Inclusivity
                StaticLocaleData
                InstanceLocaleData
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
