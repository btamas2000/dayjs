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

    let Units =
        Pattern.EnumStrings "Units" [
            "date"
            "day"
            "month"
            "year"
            "hour"
            "minute"
            "second"
            "millisecond"
        ]


    let UTC =
        Class "dayjs.utc"
        |> RequiresExternal [T<WebSharper.DayJs.Helpers.DayJsHelpers.MainResource>]
        |> WithSourceName "UTC"
        |+> Static [

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
        ]
        |+> Instance [
            "isValid" => T<unit> ^-> T<bool>
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
            "get" => Units ^-> T<string>
            "set" => Units * T<int> ^-> T<string>
            // manipulate
            
            "toString" => T<unit> ^-> T<string>
        ]

    let Assembly =
        Assembly [
            // Namespace "WebSharper.DayJs.Resources" [
            //     // DayJsResources.CustomParseFormatResource
                
            // ]
            Namespace "WebSharper.DayJs" [
                FromStandardObject
                Units
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
