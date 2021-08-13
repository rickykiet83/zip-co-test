# Assignment

Your task is to add a new feature to our existing application.

This feature is a new page to Add new Customer, place order on their behalf, and view existing customer's orders

## Backend

- Add a new route that Add/Get Orders.
- Add feature to store the status of the customer orders: InProgress, Delivered, Cancelled
- There is a maximum of 4 InProgress orders for each customer allowed to be stored. In the event of approaching display message an prevent adding new order.
- Add a new route to cancel and update an existing order.
- Add unit test

## Frontend

- Create a new page to display the list of orders for specific customer and allow to Add new order for this customer
- The page should be responsive
- There should be a textbox to search custimers by email
- Add unit test

## Notes

- You'll also notice for the sake of simplicity we're not worrying about database versioning and migrations (we'll likely discuss approaches to this you've used in the past during a subsequent technical interview).

## Getting Started
1. Install `Docker Desktop for Mac` or `Docker Desktop for Windows`.

1. Navigate to the `AspNetCorePostgreSQLDockerApp` subfolder in a console window.

1. Open the `Client` folder in a terminal window and run the following commands at the root of the folder (requires Node.js):

    - `npm install`
    - `npm install -g @angular/cli`
    - `ng build`

1. Move back up a level to the `AspNetCorePostgreSQLDockerApp` in the terminal window:

    - Run `docker-compose build`

    - Run `docker-compose up`

1. Navigate to http://localhost:5000 in your browser to view the site.
1. We recommend making sure you're able to run the app and that is working before beginning to add your new feature.

