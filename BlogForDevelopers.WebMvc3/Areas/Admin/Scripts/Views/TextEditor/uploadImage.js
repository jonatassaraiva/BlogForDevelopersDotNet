/// <reference path="/Scripts/jquery-1.6.2.js" />
/// <reference path="/Areas/Admin/Scripts/Plugins/jqueryForm/jquery.form.js" />
/// <reference path="/Areas/Admin/Scripts/Plugins/tiny_mce/tiny_mce_src.js" />

var uploadImage = new UploadImage();

function UploadImage(options) {

	function addImageInContentEditor(result) {
		var resultJson = $.parseJSON(result);

		var image = '<img src="' + resultJson.url + '" />';

		var content = tinyMCE.activeEditor.getContent();

		var contentWithImage = content + ' ' + image;

		tinyMCE.activeEditor.setContent(contentWithImage);
	}

	return {

		addContent: function (targetId, callback) {
			targetId = '#' + targetId;
			$.ajax({
				url: '/Admin/TextEditor/ImageUpload',
				cache: false,
				success: function (data) {
					$(targetId).html(data);

					callback();
				}
			});
		},

		submitForm: function (targetFormeId) {
			targetFormeId = '#' + targetFormeId;

			var options = {
				success: addImageInContentEditor
			}

			$(targetFormeId).ajaxForm(options);

			$(targetFormeId).submit();
		}
	}
}