namespace TodoistTemplateTransformer

open System
open System.IO
open FSharp.Data

type Error = 
    | InvalidFormat of string
    | MissingArgument of string 

type Result<'TSuccess, 'TFailure> =
 | Success of 'TSuccess
 | Failure of 'TFailure

type TemplateFilePath = option<string>
type TemplateStartDate = option<DateTime>

type CommandLineOptions = {
    templateFile: TemplateFilePath;
    startDate: TemplateStartDate;
}

type DaysFromNow = int

type TodoistTemplate = CsvProvider<"Template.csv">
