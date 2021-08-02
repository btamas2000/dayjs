namespace WebSharper.DayJs.Helpers

open WebSharper
open WebSharper.Core.Resources
open System
open System.Reflection

module DayJsHelpers =

    let cleanLink dHttp (url: string) =
        if dHttp && url.StartsWith("//")
            then "http:" + url
            else url

    let link dHttp (html: HtmlTextWriter) (url: string) =
        if not (String.IsNullOrWhiteSpace(url)) then
            html.AddAttribute("type", Core.ContentTypes.Text.Css.Text)
            html.AddAttribute("rel", "stylesheet")
            html.AddAttribute("href", cleanLink dHttp url)
            html.RenderBeginTag "link"
            html.RenderEndTag()
            html.WriteLine()

    let script dHttp (html: HtmlTextWriter) isModule (url: string) =
        if not (String.IsNullOrWhiteSpace(url)) then
            html.AddAttribute("src", cleanLink dHttp url)
            html.AddAttribute("type", if isModule then Core.ContentTypes.Text.Module.Text else Core.ContentTypes.Text.JavaScript.Text)
            html.AddAttribute("charset", "UTF-8")
            html.RenderBeginTag "script"
            html.RenderEndTag()

    let extendScript (advFormat: string) (html: HtmlTextWriter) =
        html.RenderBeginTag "script"
        html.Write(sprintf "dayjs.extend(window.dayjs_plugin_%s)" advFormat)
        html.RenderEndTag()

    type Kind =
        | Basic of string
        | Complex of string * list<string>

    let tryFindWebResource (t: Type) (spec: string) =
        let ok name = name = spec || (name.StartsWith spec && name.EndsWith spec)
        t.Assembly.GetManifestResourceNames()
        |> Seq.tryFind ok

    let tryGetUriFileName (u: string) =
        if u.StartsWith "http:" || u.StartsWith "https:" || u.StartsWith "//" then
            let parts = u.Split([| '/' |], StringSplitOptions.RemoveEmptyEntries)
            Array.tryLast parts
        else
            None

    type DayJsResource(kind: Kind) as this =
        let self = this.GetType()
        let name = self.FullName

        new (spec: string) =
            new DayJsResource(Basic spec)

        new (b: string, x: string, [<System.ParamArray>] xs: string []) =
            new DayJsResource(Complex(b, x :: List.ofArray xs))

        member this.GetLocalName() =
            name.Replace('+', '.').Split('`').[0]

        interface IResource with
            member this.Render ctx =
                let dHttp = ctx.DefaultToHttp
                let isLocal = ctx.GetSetting "UseDownloadedResources" |> Option.exists (fun s -> s.ToLower() = "true")
                let localFolder isCss f =
                    ctx.WebRoot + 
                    (if isCss then "Content/WebSharper/" else "Scripts/WebSharper/") + this.GetLocalName() + "/" + f
                match kind with
                | Basic spec ->
                    let mt = 
                        if spec.EndsWith ".css" then Css 
                        elif spec.EndsWith ".mjs" then JsModule 
                        else Js
                    match ctx.GetSetting name with
                    | Some url ->
                        RenderLink url
                        |> fun r ->
                            fun writer ->
                                let splitCDN =
                                    url.Split('/')
                                    |> Array.last
                                    |> fun x -> x.Split('.')
                                    |> Array.head
                                r.Emit(writer, mt, dHttp)
                                extendScript splitCDN (writer Scripts)
                    | None ->
                        match tryFindWebResource self spec with
                        | Some e -> Rendering.GetWebResourceRendering(ctx, self, e)
                        | None ->
                            if isLocal then
                                match tryGetUriFileName spec with
                                | Some f ->
                                    RenderLink (localFolder (mt = Css) f)
                                | _ ->
                                    RenderLink spec
                            else
                                RenderLink spec
                        |> fun r ->
                            fun writer ->
                                let splitCDN =
                                    spec.Split('/')
                                    |> Array.last
                                    |> fun x -> x.Split('.')
                                    |> Array.head
                                r.Emit(writer, mt, dHttp)
                                extendScript splitCDN (writer Scripts)
                | Complex (b, xs) ->
                    let b = defaultArg (ctx.GetSetting name) b
                    let urls =
                        xs |> List.map (fun x ->
                            let url = b.TrimEnd('/') + "/" + x.TrimStart('/')
                            url, url.EndsWith ".css"     
                        )  
                    let urls = 
                        if isLocal then 
                            urls |> List.map (fun (u, isCss) ->
                                match tryGetUriFileName u with
                                | Some f ->
                                    localFolder isCss f, isCss
                                | _ ->
                                    u, isCss
                            )
                        else urls
                    fun writer ->
                        for url, isCss in urls do
                            if isCss then
                                link dHttp (writer Styles) url
                            else
                                let splitCDN =
                                    url.Split('/')
                                    |> Array.last
                                    |> fun x -> x.Split('.')
                                    |> Array.head
                                script dHttp (writer Scripts) false url
                                extendScript splitCDN (writer Scripts)

    type MainResource() =
        inherit BaseResource("https://cdnjs.cloudflare.com/ajax/libs/dayjs/1.10.6/dayjs.min.js")

    [<Require(typeof<MainResource>)>]
    type CustomParseFormatResource() =
        inherit DayJsResource("https://cdnjs.cloudflare.com/ajax/libs/dayjs/1.10.6/plugin/customParseFormat.min.js")

    [<Require(typeof<MainResource>)>]
    type ObjectSupport() =
        inherit DayJsResource("https://cdnjs.cloudflare.com/ajax/libs/dayjs/1.10.6/plugin/objectSupport.min.js")