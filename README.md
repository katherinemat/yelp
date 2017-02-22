# FAKE YELP

#### A web app that helps the user bookmarks a list of restaurant by cuisines

#### By Minh Phuong and Kaz Matthews

## Description
* A website in Nancy where users can add their favorite restaurants by the type of cuisine they offer.

## Setup/Installation Requirements
* Clone From GitHub
* Open a cmd program
* Navigate to downloaded folder
* Type in cmd window "dnx kestrel"
* Enter this url in desired browser http://localhost:5004

## Specification
* The program will determine that a cuisine input text is entered, not number
  * Input: "4546"
  * Output: False
* The program will take the input name of the cuisine type and output it into a dropdown selection menu
  * Input: "Western"
  * Output: Select "Western"
* The program brings the user to a restaurant form for each cuisine
  * Input: "Western"
  * Output: Add new restaurant in western cuisine
* The user can enter fav dish, opening date, and name of restaurant
  * Input: "Lone Star", "steak", 1994-12-12
  * Output: "Lone Star", "steak", 1994-12-12
* The user can enter fav dish, opening date, and name of restaurant. This information is linked to Western cuisine through cuisine_id
  * Input: "Lone Star", "steak", 1994-12-12
  * Output: "Lone Star", "steak", 1994-12-12, 1


## Known Bugs

No known bugs

## Support and contact details

Contact me at mphuong@kent.edu and katherinematthews.polsci@gmail.com.

## Technologies Used

HTML, CSS, C#, Nancy, Razor.

### License

Copyright (c) 2017 *Minh Phuong and Kaz Matthews*
