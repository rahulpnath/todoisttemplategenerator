module TodoistTemplateGenerator.ArgParser

    open Microsoft.FSharp.Core
    open System
    open System.IO
    open System.Text.RegularExpressions

    let (|FirstRegexGroup|_|) pattern input =
        let m = Regex.Match(input,pattern) 
        if (m.Success) then Some m.Groups.[1].Value else None 

    let getDaysFromNow text = 
        match text with
        | "today" -> Success(0)
        | "tomorrow" -> Success(1)
        | FirstRegexGroup "in (\d+) days" numberOfDaysString -> 
            let numberOfDays = Int32.Parse(numberOfDaysString)
            Success(numberOfDays)
        | _ -> Result.Failure(InvalidFormat("Expected 'in x days, today or tomorrow"))

    let getDaysFromNowString daysFromNow = 
        match daysFromNow with
        | 0 -> "today"
        | 1 -> "tomorrow"
        | _ -> sprintf "in %i days" daysFromNow

    let parse args =
        let defaultOptions = { 
            templateFile = None;
            startDate = None
            }

        let isCommandLineSwitch (arg:string) = arg.StartsWith "-"

        let rec parseArguments args optionsSoFar = 
            match args with
            | [] -> Success optionsSoFar
            | "-templateFile"::xs -> 
                match xs with
                | file:string :: xss when not (isCommandLineSwitch file) -> 
                    match File.Exists file with
                    | true -> 
                        let newOptionsSoFar = {optionsSoFar with templateFile=Some(file)}
                        parseArguments xss newOptionsSoFar
                    | false -> Result.Failure(InvalidFormat("File does not exists"))
                | _ -> Result.Failure(MissingArgument("File name is required"))
            | "-startDate"::xs ->
                match xs with
                | dateString:string::xss when not (isCommandLineSwitch dateString) ->
                    match DateTime.TryParse dateString with
                    | true, date -> 
                        let newOptionsSoFar = {optionsSoFar with startDate=Some(date)}
                        parseArguments xss newOptionsSoFar
                    | _ -> Result.Failure(InvalidFormat("Invalid Date"))
                | _ -> Result.Failure(MissingArgument("Date is required"))
            | _ -> Success optionsSoFar

        parseArguments args defaultOptions

