module TodoistTemplateGenerator.ArgParser

    open Microsoft.FSharp.Core
    open System

    let parse args =
        let defaultOptions = { 
            templateFile = None;
            startDate = None
            }

        let rec parseArguments args optionsSoFar = 
            match args with
            | [] -> optionsSoFar
            | "-templateFile"::xs -> 
                match xs with
                | file:string :: xss -> 
                    let newOptionsSoFar = {optionsSoFar with templateFile=Some(file)}
                    parseArguments xss newOptionsSoFar
                | _ -> failwith "File name is required"
            | "-startDate"::xs ->
                match xs with
                | dateString:string::xss ->
                    let date = DateTime.Parse(dateString)
                    let newOptionsSoFar = {optionsSoFar with startDate=Some(date)}
                    parseArguments xss newOptionsSoFar
                | _ -> failwith "Date is required"
            | _ -> optionsSoFar

        parseArguments args defaultOptions

