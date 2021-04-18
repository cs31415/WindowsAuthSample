function addLoggedInUserLabel(userName, logonTime) {
    // create a holder for the logged in user label
    var div = document.createElement('div');
	div.setAttribute('Class', 'logged_in_user_holder');
	div.setAttribute('Id', 'logged_in_user_holder');
	div.setAttribute('style', 'position:relative;float:right;z-index:9999;padding:10px;');

    // create the label and add it to the holder
    var element = document.createElement("label");
	element.setAttribute('Id', 'logged_in_user');
	element.setAttribute('style', 'font-family: Sans-Serif; font-size: 10px; background-color:whitesmoke');
	element.innerHTML = 'Logged in as <b>' + userName + '</b> at ' + logonTime;
	div.appendChild(element);
        
    // add the holder to the end of the body
    document.body.insertBefore(div, null);
}
