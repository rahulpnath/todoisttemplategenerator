module TodoistTemplateGenerator.ArgParser

    open Microsoft.FSharp.Core
    open System
    open System.IO

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

