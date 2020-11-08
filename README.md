# .NET wrapper for ConnectedCars REST API
Inspired by [connectedcars-python](https://github.com/niklascp/connectedcars-python), rewritten in .NET Core.
Currently tested with Volkswagen and Skoda.

# Configuration
Add your accounts in "config.json" file. The current example holds an array with two accounts, as the example below:
```
{
  "Accounts": [
    {
      "Email": "email@email.com",
      "Password": "password",
      "Namespace": "semler:minskoda",
      "Id": 1
    },
    {
      "Email": "email@email.com",
      "Password": "password",
      "Namespace": "semler:minvolkswagen",
      "Id": 2
    }
  ]
}
```
Adapt the array to your needs. Should you only have one account, leave only one but keep the array.
```
{
  "Accounts": [
    {
      "Email": "email@email.com",
      "Password": "password",
      "Namespace": "semler:minskoda",
      "Id": 1
    }
  ]
}
```
## Known namespaces
- semler:minvolkswagen
- semler:minskoda
