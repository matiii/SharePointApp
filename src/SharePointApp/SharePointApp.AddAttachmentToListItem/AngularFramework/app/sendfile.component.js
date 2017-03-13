(function (angularFramework) {

    function UploadFileController(Upload) {
        var vm = this;
        vm.filesToUpload = [];

        vm.uploadFiles = function (files) {
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    console.log(files[i]);
                    vm.filesToUpload.push(files[i]);
                    //Upload.upload({..., data: {file: files[i]}, ...})...;
                }
                // or send them all together for HTML5 browsers: 
                //Upload.upload({..., data: {file: files}, ...})...;
            }
        }

        vm.sendFiles = function () {
            Upload.upload({ url: '/_layouts/SharePointApp.AddAttachmentToListItem/FileAttachmentService.asmx/UploadFile', data: { file: $scope.filesToUpload } }).then(function (resp) {
                console.log('Success');
                console.log(resp);
            }, function (resp) {
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ');
            });
        };
    }

    angularFramework.component('sendFile',
        {
            template: 'sendfile.html',
            controller: UploadFileController,
            controllerAs: 'vm'
        });

})(window.angularFramework)
