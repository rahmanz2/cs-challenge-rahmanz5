FleetCarma code challenge.

Improvements:
added a metadata file containing four lines:
1) the first line contains the private key for the edmund api (you should replace it with yours)
2) the second line contains the year for which you would like to the run the utility
3) the third line is a boolean variable; when set to true, the utility will always make an api call and pull new make data instead of reading things from the disk if you already have them (saves precious api calls)
4) the fourth line is a boolean variable; when set to true, the utility will always make an api call and pull new style data instead of reading things from the disk if you already have them

This way you don't need to modify the code for next year, and can even re run the utility for previous years. Furthermore, it is easily extensible, saves precious api call quotas, and concentrates program metadata logic into one place. Finally, develeoper specific details like the private key are not hardcoded into the utility!

The utility also organizes data into folders based on years. The make data file name is hardcoded to "edmunds-get-makes.txt", and the style data file name per vehicle is hardcoded to "edumunds-styleId-full-{styleId}.txt"

Added a little extra console logs to indicate what is happening on the utility. You have to hit a key to exit the application once the "Check file output for more detail" appears on the console.

Reduced the total number of api calls.
Now only one api call is made to save the style and determine whether the vehicle is electric, per styleId.

Perhaps this could be improved more in the future as Edmund updates their API.

Other:

I added newtonsoft.json and created classes for the Makes, Models etc. I felt like this made some program logic slightly easier for me and could be useful in the future, although it took ime to get it fully working.



