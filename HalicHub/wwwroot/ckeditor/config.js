/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    config.filebrowserImageUploadUrl = '/Admin/UploadImage'; //Resmin yükleneceði site adresi
    config.fileTools_requestHeaders = { 'X-CSRFToken': '{{ @GetAntiXsrfRequestToken() }}' };
    config.removePlugins = 'easyimage,cloudservices';//Easyimage, cloudervices eklentisini kapatmak için bu kod satýrýný ekleyin
};
