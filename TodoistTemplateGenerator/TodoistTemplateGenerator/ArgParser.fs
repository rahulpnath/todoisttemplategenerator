module ArgParser

let rec parseArguments args optionsSoFar = 
    match args with
    |[] -> optionsSoFar
    |_ -> optionsSoFar

