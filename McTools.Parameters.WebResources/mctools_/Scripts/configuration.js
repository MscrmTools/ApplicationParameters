function showParameters() {
    var url = Xrm.Page.context.prependOrgName("/main.aspx?etn=mctools_parameter&pagetype=entitylist&viewid=%7b062CB89C-40DC-4B0B-B268-A4AD7249851F%7d&viewtype=1039")
    window.open(url, 'parameters', 'toolbar=0');
}

function showBlog() {
    window.open("http://mscrmtools.blogspot.com");
}

function showDonation() {
    window.open("http://mscrmtools.blogspot.com/p/xrmtoolbox-sponsoring.html");
}

function showProject() {
    window.open("https://github.com/MscrmTools/ApplicationParameters");
}

function loadLabels() {
    var userLcid = Xrm.Page.context.getUserLcid();
    var labels = new Array();

    switch (userLcid) {
        case 1036:
            {
                labels.push({ id: "header", label: "Solution de gestion des param\350tres de configuration pour Microsoft Dynamics CRM" });
                labels.push({ id: "description", label: "Cette solution contient deux entit\351s permettant de g\351rer des param\350tres typ\351s." });
                labels.push({ id: "showParameters", label: "Cliquez ici pour afficher la liste des param\350tres" });
                labels.push({ id: "showBlog", label: "Cliquez ici pour afficher le blog MscrmTools" });
                labels.push({ id: "showDonation", label: "Cliquez ici pour afficher la page de sponsoring" });
                labels.push({ id: "showCodePlex", label: "Cliquez ici pour afficher le projet Github de cette solution" });
            }
            break;
        default:
            {
                labels.push({ id: "header", label: "Configuration settings management solution for Microsoft Dynamics CRM" });
                labels.push({ id: "description", label: "This solution contains two entities to manage typed parameters." });
                labels.push({ id: "showParameters", label: "Click here to display parameters list" });
                labels.push({ id: "showBlog", label: "Click here to display MscrmTools blog" });
                labels.push({ id: "showDonation", label: "Click here to display sponsoring page" });
                labels.push({ id: "showCodePlex", label: "Click here to display Github project page for this solution" });
            }
            break;
    }

    for (var i in labels) {
        var control = window.document.getElementById(labels[i].id);
        if (control) {
            if (control.tagName.toLowerCase() == "input") {
                control.value = labels[i].label;
            } else {
                control.innerText = labels[i].label;
            }
        }
    }
}

function htmlEncode( html ) {
    return document.createElement( 'a' ).appendChild( 
        document.createTextNode( html ) ).parentNode.innerHTML;
};