namespace WebSharper.DayJs.Extension

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =

    let DayJs =
        Class "dayjs"
        |> WithSourceName "DayJs"
        |+> Static [
            Constructor T<unit>
            Constructor T<Date>
            Constructor T<string>
            Constructor (T<string> * (T<string> + !| T<string>) * !? T<bool>)
            Constructor (T<string> * (T<string> + !| T<string>) * T<string> * !? T<bool>)
        ]
        |+> Instance [
            "toString" => T<unit> ^-> T<string>
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.DayJs.Resources" [
                Resource "DayJsCDN" "https://cdnjs.cloudflare.com/ajax/libs/dayjs/1.10.6/dayjs.min.js"
                |> AssemblyWide
            ]
            Namespace "WebSharper.DayJs" [
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
