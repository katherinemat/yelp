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
* The user can enter fav dish, opening date, and name of restaurant. This information is linked to cuisine class through cuisine_id
  * Input: "Lone Star", "steak", 1994-12-12
  * Output: "Lone Star", "steak", 1994-12-12, 1
* The user can enter fav dish, opening date, and name of many restaurants, each in linked individually through cuisine_id
  * Input: "Lone Star", "steak", 1994-12-12, "American" ; "Chipotle","burrito", 2-30-2016,"Mexican"
  * Output: "Lone Star", "steak", 1994-12-12, 1, "Chipotle","burrito", 2-30-2016,2
* The user can enter fav dish, opening date, and name of many restaurants, each in linked individually through cuisine_id and could be displayed in a list
  * Input: "Lone Star", "steak", 1994-12-12, "American" ; "Chipotle","burrito", 2-30-2016,"Mexican"
  * Output: "Lone Star","Chipotle"
* When the user click on each individual restaurant on the list, the program will return the information stored in the database for that restaurant
  * Input: Select "Lone Star"
  * Output: "Lone Star", "steak", 1994-12-12, "American"
* When the user click on "Delete All" in the list of all restaurants, the information of restaurants in the database will be cleared  
  * Input: Select "Delete All"
  * Output: "Your restaurant list is empty "
* When the user click on "Delete This Restaurant" in the individual restaurant page, the information of that particular restaurant in the database will be cleared  
  * Input: Select "Delete This Restaurant"
  * Output: Restaurant List
* When the user click on each individual cuisine on the list, the program will return the restaurant list for that cuisine
  * Input: Select "Western"
  * Output: "Lone Star", "Claim Jumper", "Red Lobster"
* When the user clicks on "Delete All" in the list of all cuisines, the information of cuisines in the database will be cleared  
  * Input: Select "Delete All"
  * Output: "Your cuisine list is empty "
* When the user click on "Delete This Cuisine" in the individual cuisine page, the information of that particular cuisine in the database will be cleared  
  * Input: Select "Delete This Cuisine"
  * Output: Cuisine List
* "Edit This Cuisine" button will allow user to change name of the individual cuisine  
  * Input: Select "Edit This Cuisine", "Western" -> "Mexican"
  * Output: "Mexican"
* "Edit This Restaurant" button will allow user to change name of the individual restaurant  
  * Input: Select "Edit This Restaurant", "Lonest" -> "Lone Star"
  * Output: "Lone Star"
* "Edit This Restaurant" button will allow user to change name, favDish, startDate, and cuisineId of the individual restaurant  
  * Input: Select "Edit This Restaurant", "Lonest", "burger", 1994-12-12, "Western" -> "Lone Star", "steak"
  * Output: "Lone Star", "steak", 1994-12-12, "Western"
* Search for a cuisine type by exact name to view all the cuisine's restaurants
  * Input: Search "Western"
  * Output: "Lone Star", "Wendys", "Claim Jumper"
* Search for a cuisine type by exact name, disregarding capitalization, to view all the cuisine's restaurants
  * Input: Search "weSTern"
  * Output: "Lone Star", "Wendys", "Claim Jumper"
* User can add a written review and rating for an individual restaurant
  * Input: "Wendys", "very good....would come again", 5 stars
  * Output: Submit
* Individual restaurant pages display written review and rating
  * Input: "Wendys"
  * Output: "very good....would come again", 5 stars
* Each restaurant displays its rating on all restaurant and cuisine pages
  * Input: all restaurants page
  * Output: "Wendys", "very good....would come again", 5 stars + "Burger King", "so freaking gross", 2 stars
* Filter restaurants by minimum rating
  * Input: 4
  * Output: "Wendys", "Il Fustino", "Ruth's Steak House", "James Brioler"

## Known Bugs

No known bugs

## Support and contact details

Contact me at mphuong@kent.edu and katherinematthews.polsci@gmail.com.

## Technologies Used

HTML, CSS, C#, Nancy, Razor.

### License

Copyright (c) 2017 *Minh Phuong and Kaz Matthews*
