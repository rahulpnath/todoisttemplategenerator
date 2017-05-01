namespace TodoistTemplateGenerator

module Main =
    open FSharp.Data
    open System

    let startInDays (now:DateTime) (newDate:DateTime) = 
        match newDate >= now with
        | true -> Success((newDate - now).TotalDays)
        | false -> Failure("New date must be greater than now")

    [<EntryPoint>]
    let main argv = 
        printfn "%A" argv
        let cla = ArgParser.parse (argv |> Seq.toList) //TODO: How to avoid this?
        let template = Template.Load("Template.csv")

        // Get start date correction days
        // For each row
            // Parse date in template to a type
            // Apply the correction date and get string

        0 // return an integer exit code
