module TodoistTemplateGenerator.ArgParser

    let parse args =
        let defaultOptions = { 
            templateFile = None;
            startDate = None
            }

        let rec parseArguments args optionsSoFar = 
            match args with
            |[] -> optionsSoFar
            |_ -> optionsSoFar

        parseArguments args defaultOptions

