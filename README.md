# ProgrammingChallenge

To run this, open in Visual Studio 2019 and hit run.
The default page is a users list, with the top 20 items.

api/User?maxRecords=50&nameSearch=Bob
The users list can be filtered with the url parameter 'maxRecords' to select more or less than 20.
Or can be searched using 'nameSearch' parameter, which will search across a users First Name, Last Name and Title.

api/User/5
This will return only the user with the given Id

api/User/Update/5
This will display a page with editable fields, to update a user.
Upon changing whichever fields, a post request will be made to the same URL with the updated user info.

api/User/Delete/5
This will delete the user with the given Id.
