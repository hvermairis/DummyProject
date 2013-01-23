var IsIEBrower = false;

var UploadImgCount = 0;




function setIEValues() {

    if (navigator.appName == 'Microsoft Internet Explorer') {
        IsIEBrower = true;

        document.getElementById('hyperAdd').style.display = '';
    }
}
//alert(IsIEBrower);

function ShowIcon() {
    var e = document.getElementById("processing");
    e.style.visibility = (e.style.visibility == 'visible') ? 'hidden' : 'visible';
}


function getCode() {
    //   // var txtAutocomplete = gettxtAutoCompleteClietId();
    //    var txtAutocomplete = '';
    //   // alert('txtAutocomplete=');//+txtAutocomplete);
    //    var e = document.getElementById(txtAutocomplete);
    //    if (e.value == 'No Data Found') {
    //        e.value = "";
    //        return;
    //    }

    // alert('value='+e.value);
}

function setVisibilityRecee(drpReceeStatus, divReceePersonRemarks, tdReceeElement, divSpecialRemarks, cellFileUploder) {

    var drpReceeStatusObj = document.getElementById(drpReceeStatus);
    var divReceePersonRemarksObj = document.getElementById(divReceePersonRemarks);
    var tdReceeElementObj = document.getElementById(tdReceeElement);
    var divSpecialRemarksObj = document.getElementById(divSpecialRemarks);
    var cellFileUploderObj = document.getElementById(cellFileUploder);
    setIEValues();
    if (drpReceeStatusObj.value == "1") {

        divReceePersonRemarksObj.style.display = "none";
        tdReceeElementObj.style.display = "";
        divSpecialRemarksObj.style.display = "";
        cellFileUploderObj.style.display = "";

    }
    else if (drpReceeStatusObj.value == "2") {
        divReceePersonRemarksObj.style.display = "";
        divSpecialRemarksObj.style.display = "";
        cellFileUploderObj.style.display = "";

        tdReceeElementObj.style.display = "none";
    }
    else {
        divReceePersonRemarksObj.style.display = "none";
        tdReceeElementObj.style.display = "none";

        divSpecialRemarksObj.style.display = "none";
        cellFileUploderObj.style.display = "none";
    }


}

function addImg() {

    var hyperAddObj = document.getElementById('hyperAdd');
    var hdnImgCountObj = document.getElementById('hdnImgCount');
    var divFileObj = document.getElementById('divFile');
    var fileListObj = document.getElementById('fileList');

    var ImgCount = parseInt(document.getElementById('hdnImgCount').value);
    ImgCount = ImgCount + 1;
    UploadImgCount = ImgCount;
    if (ImgCount <= UploadImgLimit) {
        document.getElementById('hdnImgCount').value = ImgCount.toString();

        var ImgUploder = document.createElement("input");

        ImgUploder.setAttribute('id', 'fileUImage_' + ImgCount.toString());

        ImgUploder.setAttribute('onchange', 'makeFileList(this);');

        ImgUploder.setAttribute('multiple', '');

        ImgUploder.setAttribute('type', 'file');

        ImgUploder.setAttribute('name', 'fileUImage_' + ImgCount.toString());
        // ImgUploder.setAttribute('style', 'padding-top:5px;');

        var ImgUploderBr1 = document.createElement("br");


        divFileObj.appendChild(ImgUploderBr1);


        divFileObj.appendChild(ImgUploder);


        // divFileObj.innerHTML = divFileObj.innerHTML + '<br>' + "<input type='file' onchange=\"makeFileList('fileUImage_" + ImgCount.toString() + "');\" multiple='' id='fileUImage_" + ImgCount.toString() + "' name='fileUImage_" + ImgCount.toString() + "' />";
    }
    else {

        alert('Please select Recee Images less than ' + UploadImgLimit.toString() + ' .');
    }



}

function makeFileList(input) {

    var browserName = navigator.appName;
    //    alert('browserName=' + browserName);
    //    alert('IsIEBrower=' + IsIEBrower);

    if (IsIEBrower == false) {
        UploadImgCount = input.files.length;

        if (UploadImgLimit >= UploadImgCount) {
            var ul = document.getElementById("fileList");
            while (ul.hasChildNodes()) {
                ul.removeChild(ul.firstChild);
            }

            for (var i = 0; i < input.files.length; i++) {
                var li = document.createElement("li");
                li.innerHTML = input.files[i].name;
                ul.appendChild(li);
            }
            if (!ul.hasChildNodes()) {
                var li = document.createElement("li");
                li.innerHTML = 'No Files Selected';
                ul.appendChild(li);
            }
        }
        else {

            alert('Please select Recee Images less than ' + UploadImgLimit.toString() + ' .');
        }

    }
    else {




        var divFileObj = document.getElementById('divFile');
        var hyperAddObj = document.getElementById('hyperAdd');
        var hdnImgCountObj = document.getElementById('hdnImgCount');


        var ImgCount = parseInt(document.getElementById('hdnImgCount').value);



        var ul = document.getElementById("fileList");

        //         alert('input='+input);
        //         alert('inputid='+input.id);

        var currentCount = input.id.split('_')[1];

        //alert('currentCount=' + currentCount.toString());

        var currentLi = 'li' + currentCount.toString();
        var li = document.getElementById(currentLi);

        //  alert('li=' + li);

        if (li == null) {


            li = document.createElement("li");
            li.setAttribute('id', 'li' + currentCount.toString());

            li.innerHTML = input.value;
            ul.appendChild(li);
        }
        else {
            li.innerHTML = input.value;
        }

        if (!ul.hasChildNodes()) {
            var li = document.createElement("li");
            li.innerHTML = 'No Files Selected';
            ul.appendChild(li);
        }

    }
}




function setQuantityText(drpObj, txtQuantity, lblQuantity) {

    var txtQuantityObj = document.getElementById(txtQuantity);
    var lblQuantityObj = document.getElementById(lblQuantity);

    //alert('drpObj=' + drpObj);
    //  alert('drpObj.value=' + drpObj.value);
    if (drpObj.value == '1') {
        txtQuantityObj.style.display = '';
        lblQuantityObj.style.display = '';
    }
    else {
        txtQuantityObj.style.display = 'none';
        lblQuantityObj.style.display = 'none';
    }


}


function ValidateLogin(txtUserName, txtPassword, lblErrorMsg) {

    var txtUserNameObj = document.getElementById(txtUserName);
    var txtPasswordObj = document.getElementById(txtPassword);
    var lblErrorMsgObj = document.getElementById(lblErrorMsg);

    var result = true;
    var lblErrorMsg = '';

    if (txtUserNameObj.value.trim() == '') {
        lblErrorMsg = 'Please enter UserName';
        result = false;
    }

    if (lblErrorMsg == '') {

        if (txtPasswordObj.value.trim() == '') {
            lblErrorMsg = 'Please enter Password';
            result = false;
        }
    }

    lblErrorMsgObj.innerHTML = lblErrorMsg;

    return result;

}




function ValidateOutletDetails(btnObj, txtNewOutletName, drpOutletType, txtOutletAddress) {

    var txtNewOutletNameObj = document.getElementById(txtNewOutletName);
    var drpOutletTypeObj = document.getElementById(drpOutletType);
    var txtOutletAddressObj = document.getElementById(txtOutletAddress);


    var spanNewOutletNameObj = document.getElementById('spanNewOutletName');
    var spanOutletTypeObj = document.getElementById('spanOutletType');
    var spanOutletAddressObj = document.getElementById('spanOutletAddress');

    spanNewOutletNameObj.style.display = 'none';
    spanOutletTypeObj.style.display = 'none';
    spanOutletAddressObj.style.display = 'none';

    var result = true;
    var Onfocus = false;

    if (txtNewOutletNameObj.value.trim() == '') {
        if (Onfocus == false) {
            txtNewOutletNameObj.focus();

        }
        spanNewOutletNameObj.style.display = '';
        result = false;
    }

    if (drpOutletTypeObj.value.trim() == '0') {

        if (Onfocus == false) {
            drpOutletTypeObj.focus();

        }
        spanOutletTypeObj.style.display = '';
        result = false;
    }


//    if (txtOutletAddressObj.value.trim() == '') {

//        if (Onfocus == false) {
//            txtOutletAddressObj.focus();

//        }
//        spanOutletAddressObj.style.display = '';
//        result = false;
//    }

    if (result == false) {

        alert('Please fill all the fields marked with *');
    }
    else {
        // alert(btnObj);
        //  alert(result);
        btnObj.style.display = 'none';
    }


    return result;

}
function ValidateCity(txtCityName) {

    var result = true;

    var txtCityNameObj = document.getElementById(txtCityName);

    var spanCityNameObj = document.getElementById('spanCityName');

    spanCityNameObj.style.display = 'none';

    if (txtCityNameObj.value.trim() == '') {

        txtCityNameObj.focus();
        spanCityNameObj.style.display = '';
        alert('Please fill all the fields marked with *');
        result = false;

    }

    return result;

}

function ValidateOutletName(txtOutletName) {

    var result = true;

    var txtOutletNameObj = document.getElementById(txtOutletName);

    var spanOutletNameObj = document.getElementById('spanOutletName');

    spanOutletNameObj.style.display = 'none';

    if (txtOutletNameObj.value.trim() == '') {

        txtOutletNameObj.focus();
        spanOutletNameObj.style.display = '';
        alert('Please fill all the fields marked with *');
        result = false;

    }

    return result;

}



function checkDecimalNumber(evt, txtBoxObj) {
    // alert(1);
    var carCode = (evt.which) ? evt.which : event.keyCode

    // alert('DecimalcarCode=' + carCode);



    if (carCode > 31 && ((carCode < 48) || (carCode > 57))) {
        // alert('false');
        if (carCode == 46)
            return true;
        else
            return false;
    }
    else {
        //  alert('true');
    }
}

function checkNum(evt, txtBoxObj) {
    //alert(txtBoxObj);
    var carCode = (evt.which) ? evt.which : event.keyCode

    //alert('carCode=' + carCode);
    var txtValue = '';
    if (txtBoxObj != null) {

        txtValue = txtBoxObj.value;
        if (txtValue == '') {
            if (carCode > 31 && (carCode < 49) || (carCode > 57)) {

                return false;
            }
        }
        else {

            if (carCode > 31 && (carCode < 48) || (carCode > 57)) {
                // alert('false');
                return false;
            }
        }
    }
    else {

        if (carCode > 31 && (carCode < 48) || (carCode > 57)) {
            // alert('false');
            return false;
        }
        else {
            //  alert('true');
        }
    }
}


function trim(stringToTrim) {
    return stringToTrim.replace(/^\s+|\s+$/g, "");
}



function SelectAll(checkBoxClientId) {
    // alert('checkBoxClientId='+checkBoxClientId);
    var chkText = '';
    var chktable = document.getElementById(checkBoxClientId);
    var chktr = chktable.getElementsByTagName('tr');
    var IsChecked = false;
    for (var i = 0; i < chktr.length; i++) {
        var chktd = chktr[i].getElementsByTagName('td');
        for (var j = 0; j < chktd.length; j++) {
            var chkinput = chktd[j].getElementsByTagName('input');
            var chklabel = chktd[j].getElementsByTagName('label');
            for (k = 0; k < chkinput.length; k++) {
                var chkopt = chkinput[k];
                if (chkopt.checked) {
                    var checkid = chkopt.name.substring(chkopt.name.lastIndexOf("$") + 1, chkopt.name.length);
                    if (checkid == "0") {
                        IsChecked = true;
                        break;
                    }
                }
            }
        }
    }


    for (var i = 0; i < chktr.length; i++) {
        var chktd = chktr[i].getElementsByTagName('td');
        for (var j = 0; j < chktd.length; j++) {
            var chkinput = chktd[j].getElementsByTagName('input');
            var chklabel = chktd[j].getElementsByTagName('label');
            for (k = 0; k < chkinput.length; k++) {
                var chkopt = chkinput[k];
                var checkid = chkopt.name.substring(chkopt.name.lastIndexOf("$") + 1, chkopt.name.length);
                if (checkid != "0") {
                    if (IsChecked == true) {
                        chkopt.checked = true;
                    }
                    else if (IsChecked == false) {
                        chkopt.checked = false;
                    }
                }
            }
        }
    }
}


function checkUncheckCheckBox(CheckBoxId, countLimit) {
    var firstCheckBoxId = CheckBoxId + '_0';

    //alert('firstCheckBoxId='+firstCheckBoxId);
    document.getElementById(firstCheckBoxId).checked = IsCheckCheckBox(CheckBoxId, countLimit);
}

function IsCheckCheckBox(CheckBoxId, countLimit) {

    var strcheckBoxId = "";
    strcheckBoxId = CheckBoxId + '_1';

    var IsSelected = true;
    for (count = 1; count < countLimit; count++) {
        strcheckBoxId = CheckBoxId + '_' + count;
        // alert(strcheckBoxId);
        if (!document.getElementById(strcheckBoxId).checked) {
            IsSelected = false;
            break;

        }
    }

    return IsSelected;

}

function checkUncheckAllCheckBox(CheckBoxId, countLimit, chkState, StateLimit, chkcity, cityLimit) {
    var firstCheckBoxId = CheckBoxId + '_0';
    var IsSelected = true;
    // alert('firstCheckBoxId='+firstCheckBoxId);
    if (!IsNothingCheckCheckBox(CheckBoxId, countLimit))//reportType , 1=India Level,2 = State Level , 3= City Level 
    {
        alert('Please select any region');

        IsSelected = false;
    }

    if (IsSelected == true && !IsNothingCheckCheckBox(chkState, StateLimit))//reportType , 1=India Level,2 = State Level , 3= City Level 
    {

        alert('Please select any branch');

        IsSelected = false;
    }

    if (IsSelected == true && !IsNothingCheckCheckBox(chkcity, cityLimit))//reportType , 1=India Level,2 = State Level , 3= City Level 
    {
        alert('Please select any city');

        IsSelected = false;

    }

    return IsSelected;
}

function IsNothingCheckCheckBox(CheckBoxId, countLimit) {

    var strcheckBoxId = "";
    strcheckBoxId = CheckBoxId + '_1';

    var countCheck = 0;
    var IsSelected = false;
    for (count = 1; count < countLimit; count++) {
        strcheckBoxId = CheckBoxId + '_' + count;
        // alert(strcheckBoxId);
        if (document.getElementById(strcheckBoxId).checked) {
            countCheck = countCheck + 1;
        }

    }

    if (countCheck > 0) {
        IsSelected = true;
    }

    //alert('IsSelected='+IsSelected);
    return IsSelected;

}


function ErrorMsgAlert(msg) {
    //alert(1);
    alert(msg);
}


function setOthersVisible(drpSurfaceObj, cellHeadingSurFaceText) {

    var cellHeadingSurFaceTextObj = document.getElementById(cellHeadingSurFaceText);


    if (drpSurfaceObj.value == "8") {//Others

        cellHeadingSurFaceTextObj.style.display = '';
    }
    else {
        cellHeadingSurFaceTextObj.style.display = 'none';

    }
}


function setInstallation(drpInstallationObj) {//divInstallation

    var divInstallationObj = document.getElementById('divInstallation');



    if (drpInstallationObj.value == 'Timing') {

        divInstallationObj.style.display = '';
    }
    else {
        divInstallationObj.style.display = 'none';
    }


}

function setDeliveryTruckReach(drpDeliveryTruckReachObj, txtTruckDistance) {//trTruckDistance

    var trTruckDistancenObj = document.getElementById('trTruckDistance');
    var txtTruckDistanceObj = document.getElementById(txtTruckDistance);

    if (drpDeliveryTruckReachObj.value == '0') {
        trTruckDistancenObj.style.display = '';
    }
    else {
        txtTruckDistanceObj.value = '';
        trTruckDistancenObj.style.display = 'none';
    }

}
