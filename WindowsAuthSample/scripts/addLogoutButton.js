function addLogoutButton() {
    // create a holder for the button
    var div = document.createElement("div");
    div.setAttribute('Class', 'logout_button_holder');
    div.setAttribute('Id', 'logout_button_holder');
    div.setAttribute('style', 'position:relative;float:right;z-index:9999');

    // create the button and add it to the holder
    var element = document.createElement("button");
    element.setAttribute('type', 'button');
    element.setAttribute('Id', 'logout_button');
    element.setAttribute('style', 'font-family: Sans-Serif; font-size: 11px; background-color: darkorange; border: none; color: white; padding: 5px 20px; text-align: center; display: inline-block; cursor:pointer;');
    element.textContent = 'Logout';
    element.onclick = function() {
        window.location.href = "/admin/logout.aspx"; 
    };
    
    div.appendChild(element);

    // add the holder to top of the page
    document.body.insertBefore(div, document.body.childNodes[0]);
}