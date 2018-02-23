# ShoppingAssistant

An individual third year project implementing a grocery comparison app whilst demonstrating a wide variety of skills.

This is a client application for use with the ShoppingAssistantApi. The application stores a local database of shopping lists and interacts with a remote API to provide price information at Tesco and Iceland locations. Information is only provided for locations nearby the user to reduce data transmissions. The application also offers functionality to compare prices for nearby locations for the entirety of a shopping list giving a detailed breakdown of which items to be, the ability to share shopping lists with others users, and an interface to add a collection of items to a shopping list from a recipe selected using the Edamam API.

It is developed using the Xamarin PCL cross platform library to target the maximum number of devices. Dependencies have however not been created for iOS deployment due to lack of a Mac development machine. The application demonstrates asynchronous programming using the async and await operators in C#. The LINQ library is used throughout the project along with the MVC design pattern. More attention should have been paid to the MVVM pattern and reducing code through inheritance in the controllers. Notifications were also implemented using background tasks that poll the remote API but should use a push notification service such as Firebase Cloud Messaging or Azure Notification Hubs.