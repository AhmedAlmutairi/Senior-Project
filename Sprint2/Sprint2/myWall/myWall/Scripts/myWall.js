function TodoList() {
    var item = document.getElementById("TodoInput").value
    var text = document.createTextNode(item)
    var newitem = document.createElement("li")
    newitem.appendChild(text)
    document.getElementById("TodoList").appendChild(newitem)
}