namespace TodoistTemplateGenerator

open System
open System.IO

type TemplateFilePath = option<string>
type TemplateStartDate = option<DateTime>

type CommandLineOptions = {
    templateFile: TemplateFilePath;
    startDate: TemplateStartDate;
}

