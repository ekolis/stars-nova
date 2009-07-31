document.write("<br />")
document.write("<hr />")

var days 	= new Array(8);
days[1] 	= "Sunday";
days[2] 	= "Monday";
days[3] 	= "Tuesday";
days[4] 	= "Wednesday";
days[5] 	= "Thursday";
days[6] 	= "Friday";
days[7] 	= "Saturday";

var months 	= new Array(13);
months[1] 	= "January";
months[2] 	= "February";
months[3] 	= "March";
months[4] 	= "April";
months[5] 	= "May";
months[6] 	= "June";
months[7] 	= "July";
months[8] 	= "August";
months[9] 	= "September";
months[10] 	= "October";
months[11] 	= "November";
months[12] 	= "December";

var dateObj 	= new Date(document.lastModified)
var wday 	= days[dateObj.getDay() + 1]
var lmonth 	= months[dateObj.getMonth() + 1]
var date 	= dateObj.getDate()
var fyear 	= dateObj.getYear()

if (fyear < 2000) 
   fyear = fyear + 1900

document.write("Copyright © Ken Reed, 2008. Last modified " + wday + ", " + lmonth + " " + date + ", " + fyear + 
"<a href=\"http://sourceforge.net\"> <img src=\"http://sflogo.sourceforge.net/sflogo.php?group_id=188744&amp;type=1 \" border=\"0\" align=\"left\" a>"
)