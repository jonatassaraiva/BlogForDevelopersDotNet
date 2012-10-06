/// <reference path="/Scripts/jquery-1.6.2.js" />
/// <reference path="/Areas/Admin/Scripts/Views/TextEditor/uploadImage.js" />
/// <reference path="/Areas/Admin/Scripts/Plugins/tiny_mce/tiny_mce_src.js" />

var createPost = new CreatePost();

function CreatePost() {

    function callBackUpalodImage() {

        $("#form-image-upload input[type=file]").filestyle({
            image: "/Areas/Admin/Scripts/Plugins/jqueryFileStyle/folder.png",
            imageheight: 27,
            imagewidth: 64,
            width: 255,
        });

        $('#form-image-upload').validate({
            errorLabelContainer: $("#mws-form-message-upload-image"),
            wrapper: "li",
            rules: {
                'imageUpload-filestyle': {
                    required: true
                }
            },
            messages: {
                'imageUpload-filestyle': "Image is required"
            }
        });

        $('#mws-jui-dialog-upload').dialog('open');
    }

    function initDiologUploadImage() {
        $('#mws-jui-dialog-upload').dialog({
            autoOpen: false,
            title: 'Upload image',
            modal: true,
            width: 440,
            height: 250,
            position: ['auto', 50],
            buttons: [{
                text: " Upload ",
                click: function () {
                    var elementFormId = 'form-image-upload';

                    var bValid = $('#' + elementFormId).valid();

                    if (bValid) {
                        uploadImage.submitForm(elementFormId);

                        $(this).dialog("close");
                    }
                }
            },
			{
			    text: "Cancel",
			    click: function () {
			        $(this).dialog("close");
			    }
			}]
        });
    }

    function initDialog(suffix, title) {
        $('#mws-jui-dialog-' + suffix).dialog({
            autoOpen: false,
            title: title,
            modal: true,
            width: 640,
            height: 300,
            position: ['auto', 50],
            buttons: [{
                text: " Add ",
                click: function () {
                    var contentTags = $('#' + suffix).val();
                    var tagsChecked = $('#list-' + suffix + ' input:checked');

                    $(tagsChecked).each(function () {
                        if (contentTags.length == 0) {
                            contentTags += $(this).val();
                        }
                        else {
                            contentTags += ', ' + $(this).val();
                        }
                        $(this).attr('checked', false);
                    });

                    $('#' + suffix).val(contentTags);

                    $(this).dialog("close");
                }
            }]
        });
    }

    function initBtnUploadImage() {
        $("#mws-growl-btn-add-image").bind("click", function (event) {
            var targetId = 'mws-dialog-inner-upload';

            $('#mws-dialog-inner-upload').empty();

            uploadImage.addContent(targetId, callBackUpalodImage);
        });
    }

    function initBtnTags() {
        $("#mws-growl-btn-tags").bind("click", function (event) {
            $("#mws-jui-dialog-tags").dialog("open");
            event.preventDefault();
        });
    }

    function initValidateForm() {
        $("#createPost").validate({
            errorLabelContainer: $("#mws-validate-error"),
            wrapper: "li",
            rules: {
                Title: {
                    required: true
                }
            },
            messages: {
                Title: "Title is required"
            }
        });
    }

    function initEditor() {

        // Default skin
        tinyMCE.init({
            // General options
            mode: "exact",
            elements: "Content",
            theme: "advanced",
            plugins: "syntaxhl,autolink,lists,pagebreak,layer,table,save,advhr,emotions,iespell,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,xhtmlxtras,",

            // Theme options
            theme_advanced_buttons1: "syntaxhl,fullscreen,code,cleanup,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,formatselect,fontselect,fontsizeselect,|,bullist,numlist,|,outdent,indent,blockquote",
            theme_advanced_buttons2: "cite,undo,redo,|,cut,copy,paste,pastetext,pasteword,|,search,link,unlink,anchor,image,media,inserttime,|,forecolor,backcolor,|,advhr,|,preview,print,|,ltr,rtl,hr,removeformat,sub,sup,charmap,emotions",
            theme_advanced_buttons3: "tablecontrols",
            

            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,
            heme_advanced_resize_horizontal: false,


            // Drop lists for link/image/media/template dialogs
            template_external_list_url: "lists/template_list.js",
            external_link_list_url: "lists/link_list.js",
            external_image_list_url: "lists/image_list.js",
            media_external_list_url: "lists/media_list.js",

            convert_urls: false,

            theme_advanced_path: false,
            setup: function (ed) {
                ed.onKeyUp.add(function (ed, e) {

                    var strip = (tinyMCE.activeEditor.getContent()).replace(/(<([^>]+)>)/ig, "");
                    var text = strip.split(' ').length + " Words, " + strip.length + " Characters"
                    tinymce.DOM.setHTML(tinymce.DOM.get(tinyMCE.activeEditor.id + '_path_row'), text);
                });
            }
        });
    }

    return {

        init: function () {
            initDiologUploadImage();
            initBtnUploadImage();

            initEditor();

            initDialog("tags", "Tags");
            initBtnTags();

            initValidateForm();
        },

        buildTags: function (tags) {
        	$('#tags').val(tags);
        }
    }
}