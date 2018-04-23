//onclick="JavaScript: downloadFDF('fdfdata.fdf');"
// PdForms.net - An open source pdf form editor
// Copyright 2018 Nicholas Kowalewicz All Rights reserved.
// PdForms.net utilizes iTextSharp technologies.
// Email Contact: nick.kowalewicz [at] gmail.com
// Website: www.pdforms.net
function generateFDF(pdfPath) {
    var str = '';
    var strVals = '';
    var elem = document.getElementById('form').elements;
    for (var i = 0; i < elem.length; i++) {
        //str += "<b>Type:</b>" + elem[i].type + "&nbsp&nbsp";
        //str += "<b>Name:</b>" + elem[i].name + "&nbsp;&nbsp;";
        //str += "<b>Value:</b><i>" + elem[i].value + "</i>&nbsp;&nbsp;";
        //str += "<BR>";
        //str += "" + elem[i].type + "&nbsp&nbsp";
        console.log(elem[i].id + "," + elem[i].type);
        if (elem[i].type == null) { }
        else if (elem[i].type == 'select-multiple' | elem[i].type == 'select-one') {
            strVals += " << ";
            if (elem[i].name != null) {
                strVals += " /T(" + elem[i].name + ") ";
            } else if (elem[i].id != null) {
                strVals += " /T(" + elem[i].id + ")";
            }
            var x = document.getElementById(elem[i].id);
            strVals += " /V [";
            for (var y = 0; y < x.options.length; y++) {
                if (x.options[y].selected == true) {
                    strVals += "(" + (x.options[y].value) + ")";
                }
            }
            strVals += "] ";
            strVals += " >> ";
        }
        else if (elem[i].type == 'radio') {
            if (elem[i].checked) {
                strVals += " << ";
                if (elem[i].name != null) {
                    strVals += " /T(" + elem[i].name + ") ";
                } else if (elem[i].id != null) {
                    strVals += " /T(" + elem[i].id + ")";
                }
                strVals += " /V(" + elem[i].value + ")";
                strVals += " >> ";
            };
        }
        else if (elem[i].type == 'checkbox') {
            strVals += " << ";
            if (elem[i].name != null) {
                strVals += " /T(" + elem[i].name + ") ";
            } else if (elem[i].id != null) {
                strVals += " /T(" + elem[i].id + ")";
            };
            if (elem[i].checked) {
                strVals += " /V(" + elem[i].value + ")";
            } else {
                strVals += " /V(" + "Off" + ")";
            };
            strVals += " >> ";
        }
        else {
            strVals += " << ";
            if (elem[i].name != null) {
                strVals += " /T(" + elem[i].name + ") ";
            } else if (elem[i].id != null) {
                strVals += " /T(" + elem[i].id + ")";
            }
            strVals += " /V(" + elem[i].value + ")";
            strVals += " >> ";
        }
        //else if (elem[i].type == 'text') { }
        //else if (elem[i].type == 'number') { }
        //else if (elem[i].type == 'date') { }
        //else if (elem[i].type == 'time') { }
        //else if (elem[i].type == 'file') { }
        //else if (elem[i].type == 'image') { }

    }
    str += "%FDF-1.2\r\n";
    str += "1 0 obj";
    str += "<< /FDF <<  /Fields[";
    str += strVals;
    str += "]  /F (" + pdfPath + ") >> >>";
    str += "\r\n";
    str += "\r\n" + "endobj";
    str += "\r\n" + "trailer";
    str += "\r\n" + "<< /Root 1 0 R>>";
    str += "\r\n" + "%%EOF";
    //document.getElementById('lblValues').innerHTML = str;
    //downloadFDF(str);
    return (str + "")
}
function downloadFDF(fileName) {
    var fdfdata = generateFDF("{ PDFPATH }");
    try {
        var dataUri = "data:application/vnd.fdf;charset=utf-8," + encodeURIComponent(fdfdata);
        //var filename = "data.fdf"
        //F:\\Data\\PROGRAMMING\\VS.Net\\2017\\PDForms.net\\bin\\Release\\pe.pdf
        $("<a download='" + fileName + "' href='" + dataUri + "'></a>")[0].click();
        //var ele = document.createElement("a");
        //a.setAttribute("id", 'downloadFDF').setAttribute("download", filename).setAttribute("href", dataUri);
        //a.click = new function () {
        //    //document.removeChild(document.getElementById("downloadFDF"));
        //    //this.preventDefault();
        //    setTimeout("document.removeChild(document.getElementById('downloadFDF'));", 1000);                   
        //};
        //a.onload = new function (e) {
        //    //document.getElementById("downloadFDF").click()
        //    this.click();
        //};

    } catch (dl) {
        alert(dl.description);
        document.location = 'data:Application/octet-stream,' + encodeURIComponent(fdfdata);
        //alert(dl.description); 
    };
    //iframe.src = url;
};