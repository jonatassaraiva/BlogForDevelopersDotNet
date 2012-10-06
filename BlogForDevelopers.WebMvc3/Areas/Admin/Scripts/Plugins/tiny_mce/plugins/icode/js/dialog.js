tinyMCEPopup.requireLangPack();

var icodeDialog = {
    init: function() {
        var f = document.forms[0];

        // Get the selected contents as text and place it in the input
        //f.someval.value = tinyMCEPopup.editor.selection.getContent({format : 'text'});		
    },

    insert: function() {
        // Insert the contents from the input into the document
        tinyMCEPopup.editor.execCommand('mceInsertContent', false, GetFormatedCode());
        tinyMCEPopup.close();
    }
};

function GetFormatedCode() {
    var strCode = document.forms[0].txtCode.value;

    strCode = strCode.replace(/</gi,"&lt;");
    strCode = strCode.replace(/>/gi, "&gt;");
    //strCode = strCode.replace(/&gt;/gi, ">");
    var strCodeText = '<pre class="brush: ' + document.forms[0].selctLanguage.value + '" name="code">';
    strCodeText += strCode;
    strCodeText += '</pre><br/>'    
    return strCodeText;
    //alert("done");
}

tinyMCEPopup.onInit.add(icodeDialog.init, icodeDialog);
