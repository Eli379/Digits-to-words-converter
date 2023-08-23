# TechOne-Tech-test
This is an API that converts numbers to words.
Use the attached html file to interact with the API.

## How to build
1. Clone the repository.
2. Open the solution in Visual Studio.
3. Build the solution.

## Trying out the API
1. Run the project on visual studio
2. Open the attached html file and make sure at around line 27 that the url matches the url the project will run on in visual studio
3. Open the html and begin your number to word conversions!

## Keep in mind
Entering anything besides an integer or decimal will make the API return nothing

## My Approach

#Input Validation
Created a ConversionRequest Model in order for the passed data to serialised through JSON. This approach provides a clear and standardized way of handling input validation while leveraging the built-in features of ASP.NET Core.

#Separation of Conversion Logic
The core logic of converting the numbers to words is encapsulated within 3 methods: ConvertToWords, ConvertThreeDigitGroup, and ConvertTwoDigitGroup. Makes it modular and allows for future potential testing and future enhancements.

#Logic
The ConvertThreeDigitGroup function is responsible for converting a three-digit group of a number (e.g., thousands, millions) into its word representation. It's utilized by the ConvertToWords function to handle the conversion of different segments of a given number.

The ConvertTwoDigitGroup function is designed to convert a two-digit group of a number into its word representation. It is used by both the ConvertThreeDigitGroup and ConvertToWords functions for handling the conversion of tens and teens.

The ConvertToWords function is self explanatory it converts the decimal numbers into words
