function addRow(bistade, antal, dato, varighed, kommentar) {
    if (!document.getElementsByTagName) return;
    var date = new Date(dato);
    tabBody = document.getElementsByTagName("tbody").item(0);
    row = document.createElement("tr");
    cell1 = document.createElement("td");
    cell2 = document.createElement("td");
    cell3 = document.createElement("td");
    cell4 = document.createElement("td");
    cell5 = document.createElement("td");
    textnode1 = document.createTextNode(bistade);
    textnode2 = document.createTextNode(antal);
    textnode3 = document.createTextNode(date.toDateString());
    textnode4 = document.createTextNode(varighed);
    textnode5 = document.createTextNode(kommentar ? kommentar : "");
    cell1.appendChild(textnode1);
    cell2.appendChild(textnode2);
    cell3.appendChild(textnode3);
    cell4.appendChild(textnode4);
    cell5.appendChild(textnode5);
    row.appendChild(cell1);
    row.appendChild(cell2);
    row.appendChild(cell3);
    row.appendChild(cell4);
    row.appendChild(cell5);
    tabBody.appendChild(row);


}



$.getJSON("/api/VarroCounts/", null, function(items, textStatus, jqXHP) {
    var i;
    var htmlStr = "";
    for (i = 0; i < items.length; i++) {
        addRow(items[i].Bistade, items[i].MiteCount, items[i].Date, items[i].ObservationTime, items[i].Comment);
    }
    console.log(items);
});