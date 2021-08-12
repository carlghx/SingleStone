# SingleStone .NET Coding Challenge

I initially tried to implement this using a local SQLExpress AdventureWorks db and the .NET entity data model. I had trouble getting this to work (something about The type or namespace name could not be found), so I wrote a quick hardcoded version just to test the front end, before finally switching to LiteDB which worked perfectly. I probably should have gone with LiteDB from the start, but I hesitated because I haven't worked with LiteDB before.

Contents of this solution:

AdventureWorksModel - this does not work and is only here for posterity.

DataAccess - This contains 3 classes for retrieving data: AdventureWorksData.cs (doesn't work), MockData (hardcoded data), and LiteDbData (the solution I went with in the end). It also contains a test class, LiteDBTests.

LiteDB assumes the existence of a C:\Temp folder. The NUnit class uses C:\Temp\NUnitUseOnly.db and the Web application uses C:\Temp\Api.db

RestApi - This was created using the Visual Studio Web Api template. The relevant API calls are in Controllers/ContactsController.

Testing on the front end was done using Postman with the following:
https://localhost:44345/contacts/1
{
  "name": {
    "first": "string",
    "middle": "string",
    "last": "string" 
  },
  "address": {
    "street": "string",
    "city": "string",
    "state": "string",
    "zip": "string"
  },
  "phone": [
    {
      "number": "string",
      "type": "home"
    }
  ],
  "email": "string"
}

