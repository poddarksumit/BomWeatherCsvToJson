# BomWeatherCsvToJson
 
This project is an assessment for an oppurtunity. The project has a console application build on .Net Core 3.0, along with a NUnit test project.

## What's Inside?

This project includes:

- BomWeatherCsvToJson
  - Console application build on .Net Core 3.0.
  - The application 
    - takes a BOM CSV file path from user
    - validated the path
    - reads the CSV file data and convert it into List
    - process every records and convert into an object for JSON
    - dumps the data into a file as JSON.
  - The jobs mentioned above are split using individual handlers (path validation, csv helper and file writer) and are injected through DI.
  - The code are split into the feature in order to make it easily testable.
- BomWeatherCsvToJsonTests
  - Test individual handlers like FilePathValidator and CSVFileReader.
  - Test the actual class chunks the data from CSV and builds a JSON object.

### Packages

This project uses the following key nuget packages

- CSVHelper
  - A very easy and effective way to read a CSV file.
  - Comes with alot of feature and ways to read a file.
  - Column to object mapping can be easily done by using annotations.
  - For more check: https://joshclose.github.io/CsvHelper/getting-started
``` c#
// To read the file data and map into the object
using (var reader = new StreamReader(filePath))
using (var csv = new CsvReader(reader))
{
    csv.Configuration.HasHeaderRecord = true;
    records = csv.GetRecords<WeatherData>();
}

// To map the column to field.
[Name("Bureau of Meteorology station number")]
public string BomStationNumber { get; set; }
```
- Newtonsoft.Json
  - For object to JSON operations.
 
- Microsoft.Extensions.*
  - For loading the configuration.
  - For DI.
 
 ### How to run
 
- Once the code is downloaded, please restore the packages (if not done automatically) and then rebuild the solution.
- Upon running the solution a CMD window will prompt for the path of the location. You can provide the CSV file added to the project i.e  ../../../Model/Input/IDCJAC0009_066062_1800_Data.csv
- Once the CSV file is processed, the JSON feed will be generated at the location mentioned in the config file which will be based on the DataTime (eg /Model/Output/Json/BomJsonFeed-20191117T17:56:24.json)
- The feed generated is intended to be:
  - in JSON intented format
  - in descending order by Year

### Steps to process the CSV-to-JSON conversion

- The project initiates by locaing the appsettings.json and configuring the services (like handlers).
- It will pull the instance of ProcessCsvToJson and call a main method to process the records.
- Processing record follows through these handlers and failure to any will terminate with an exception, which is handled in the main class
  - FilePathValidator: Validates the path of the CSV file: empty/null, right extension, exists.
  - CsvFileHandler: Gets the path and using CSVHepler extension converts the data into the List of model.
  - Process every records using LINQ and build the output object for JSON.
  - FileWriterHandler: Writes the JSON data in the file mentioned in the config file.
