﻿let index = 0;

function AddTag() {
    var tagEntry = document.getElementById("TagEntry");

    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        //Swal.fire({
        //    title: 'Error!',
        //    text: 'Do you want to continue',
        //    icon: 'error',
        //    confirmButtonText: 'Cool'
        //});
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });
    }
    else {
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;
    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>Choose a tag before deleting!</span>"
        });
        return true;
    }
    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else
            tagCount = 0;
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        ReplaceTag(tagArray[loop], loop);
        indexx++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

function search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsEl = document.getElementById('TagList');
    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `The Tag #${str} was detected as a duplicate and not permitted!`
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
    },
    imageUrl: '/images/error.jpg',
    timer: 3000,
    buttonsStyling: false
});