import {ICustomer} from "../app/shared/interfaces";

export const CUSTOMERS: ICustomer[] = [
  {
    id: 1,
    firstName: "Michelle 123",
    lastName: "Avery",
    email: "Michelle.Avery@acmecorp.com",
    address: "346 Cedar Ave.",
    city: "Dallas",
    state: null,
    zip: 85237,
    gender: "Female",
    orderCount: 2,
    orders: [
      {
        "id": 1,
        "product": "Needes",
        "quantity": 1,
        "price": 5.99,
        "customerId": 1,
        "status": "InProgress"
      },
      {
        "id": 2,
        "product": "Speakers",
        "quantity": 1,
        "price": 499.99,
        "customerId": 1,
        "status": "InProgress"
      },
    ],
    latitude: 0,
    longitude: 0
  },
  {
    id: 2,
    firstName: "Ward",
    "lastName": "Bell",
    "email": "Ward.Bell@gmail.com",
    "address": "12 Ocean View St.",
    "city": "Dallas",
    "state": null,
    "zip": 85233,
    "gender": "Male",
    orderCount: 2,
    orders: [
      {
        "id": 3,
        "product": "Needes",
        "quantity": 1,
        "price": 5.99,
        "customerId": 2,
        "status": "Delivered"
      }
    ],
    latitude: 0,
    longitude: 0
  },
];
