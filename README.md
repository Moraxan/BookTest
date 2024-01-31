Rubber ducky:

This backend code lets the user add Authors and books. One author can have many books and one book can have many authors. A delete of a book or an author cascades so they're also deleted from the join tables.
There is functionality to add quotations for the users. These are meant to be linked to books, but that is not implemented yet.
I've implemented basic authorization code. Users can have tokens. Need to dive deeper into that.

The backend uses a service interface which is injected into controllers. There are entities for handling the database and DTOs for handling user interactions with the database.

This should be connected to a frontend which doesn't exist at the moment.
