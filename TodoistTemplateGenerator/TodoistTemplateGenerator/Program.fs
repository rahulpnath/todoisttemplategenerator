namespace TodoistTemplateGenerator

module Main =
    open FSharp.Data
    open System
    open System.IO

    let startInDays (now:DateTime) (newDate:DateTime) = 
        match newDate >= now with
        | true -> Success((newDate - now).TotalDays)
        | false -> Failure("New date must be greater than now")
    
    let AdjustDate current days =
        let currentDays = ArgParser.getDaysFromNow current
        match currentDays with
        | Success day -> 
            ArgParser.getDaysFromNowString (day + days)
        | _ -> current
    
    let getNewName name (date:DateTime) =
        let dirName = Path.GetDirectoryName name
        let extn = Path.GetExtension name
        let fileName = Path.GetFileNameWithoutExtension name
        let newFileName = sprintf "%s_%s" fileName (date.ToString("ddMMyyyy"))
        Path.Combine(dirName, newFileName + extn)

    [<EntryPoint>]
    let main argv = 
        let cla = ArgParser.parse (argv |> Seq.toList) //TODO: How to avoid this like using IEnumerable?
        match cla with
        | Failure message -> 
            printfn "%A" message
            printfn  "\nUsage: \n\
            TodoistTemplateGenerator -startDate \"03 Oct 2014\" -templateFile \"<pathToFile>\" \n\n\
            Outputs a new file with the dateAppended to file name in the same folder as the existing file."
        | Success options -> 
            let template = TodoistTemplate.Load(options.templateFile.Value)
            let adjustByDays = startInDays DateTime.Now options.startDate.Value
            match adjustByDays with
            | Failure message -> printfn "%s" message
            | Success days ->
                let newFile = getNewName options.templateFile.Value options.startDate.Value
                template.Map(fun row ->
                    TodoistTemplate.Row(
                        row.TYPE,
                        row.CONTENT,
                        row.PRIORITY,
                        row.INDENT,
                        row.AUTHOR,
                        row.RESPONSIBLE,
                        AdjustDate row.DATE (Convert.ToInt32(days)),
                        row.DATE_LANG,
                        row.TIMEZONE))
                    .Save(newFile)

        0 // return an integer exit code
