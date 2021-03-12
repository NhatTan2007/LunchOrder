
$('.navTrigger').click(function () {
    $(this).toggleClass('active');
    console.log("Clicked menu");
    $("#mainListDiv").toggleClass("show_list");
    $("#mainListDiv").fadeIn();

});
//< !--Function used to shrink nav bar removing paddings and adding black background-- >
$(window).scroll(function () {
    if ($(document).scrollTop() > 50) {
        $('.nav').addClass('affix');
        console.log("OK");
    } else {
        $('.nav').removeClass('affix');
    }
});

(function ($) {
    'use strict';
    /*==================================================================
        [ Daterangepicker ]*/
    try {
        $('.js-datepicker').daterangepicker({
            "singleDatePicker": true,
            "showDropdowns": true,
            "autoUpdateInput": false,
            locale: {
                format: 'DD/MM/YYYY'
            },
        });

        var myCalendar = $('.js-datepicker');
        var isClick = 0;

        $(window).on('click', function () {
            isClick = 0;
        });

        $(myCalendar).on('apply.daterangepicker', function (ev, picker) {
            isClick = 0;
            $(this).val(picker.startDate.format('DD/MM/YYYY'));

        });

        $('.js-btn-calendar').on('click', function (e) {
            e.stopPropagation();

            if (isClick === 1) isClick = 0;
            else if (isClick === 0) isClick = 1;

            if (isClick === 1) {
                myCalendar.focus();
            }
        });

        $(myCalendar).on('click', function (e) {
            e.stopPropagation();
            isClick = 1;
        });

        $('.daterangepicker').on('click', function (e) {
            e.stopPropagation();
        });


    } catch (er) { console.log(er); }
    /*[ Select 2 Config ]
        ===========================================================*/

    try {
        var selectSimple = $('.js-select-simple');

        selectSimple.each(function () {
            var that = $(this);
            var selectBox = that.find('select');
            var selectDropdown = that.find('.select-dropdown');
            selectBox.select2({
                dropdownParent: selectDropdown
            });
        });

    } catch (err) {
        console.log(err);
    }


})(jQuery);

var terms = document.getElementById("terms");
if (terms != null) {
    terms.addEventListener("change", checkterms);
    function checkterms() {
        if (terms.checked) {
            document.getElementById("submit-btn").disabled = false;
        }
        else {
            document.getElementById("submit-btn").disabled = true;
        };
    };
}

function confirmChangeOrder() {
    return confirm('Bạn chắc chắn muốn đổi món ăn này không?')
}

function confirmDeleteFood() {
    return confirm('Bạn chắc chắn muốn xóa món ăn này không?')
}

function existsFoodInOrder() {
    let date = new Date()
    let day = ""
    let month = ""
    if (date.getDate().toString().length == 1) {
        day = "0" + date.getDate()
    } else if (date.getDate().toString().length == 2) {
        day = date.getDate()
    }
    if (date.getMonth().toString().length == 1) {
        month = "0" + (date.getMonth() + 1)
    } else if (date.getDate().toString().length == 2) {
        month = (date.getDate() + 1).toString()
    }
    let fullDate = `${day}/${month}/${date.getFullYear()}`
    alert(`Bạn đã đặt món ăn này trong ngày ${fullDate}`)
    return false
}

// Add the following code if you want the name of the file appear on select
let customFile = document.getElementById("customFile")
if (customFile != null) {
    customFile.addEventListener("change", changeImages)
}
function changeImages() {
let thumbnail = document.getElementById("thumbnail")
    thumbnail.src = URL.createObjectURL(customFile.files[0])
    thumbnail.style.maxWidth = "250px"
    thumbnail.style.maxHeight = "250px"
}
const pageLinks = document.getElementsByClassName("page-link");
if (pageLinks.length > 0) {
    
    const currentLocation = location.href;
    for (var i = 2; i < pageLinks.length - 2; i++) {
        pageLinks[i].parentElement.classList.remove("active");
        if (pageLinks[i].href === currentLocation) {
            pageLinks[i].parentElement.classList.add("active")
        }
    }

    if (currentLocation === pageLinks[0].href || currentLocation === `${window.location.origin}/`) {
        pageLinks[0].parentElement.classList.add("disabled")
        pageLinks[1].parentElement.classList.add("disabled")
    }

    if (currentLocation == pageLinks[pageLinks.length - 1].href) {
        pageLinks[pageLinks.length - 1].parentElement.classList.add("disabled")
        pageLinks[pageLinks.length - 2].parentElement.classList.add("disabled")
    }
}

const defaultPage = 1;
const defaultPageSize = document.getElementById("pageSize") != null ? document.getElementById("pageSize").value : 0;
const domain = window.location.origin;
const path = window.location.pathname;
let pathArr = path.split("/");

function changePageSize(el) {
    if (path == "/") {
        window.location.href = `/home/index/${defaultPage}/${el.value}`;
    } else {
        window.location.href = `${domain}/${pathArr[1]}/${pathArr[2]}/${defaultPage}/${el.value}`;
    }
}
//search function
function searchKeyWord(el) {
    if (path == "/") {
        window.location.href = `/home/index/${defaultPage}/${defaultPageSize}/${el.value}`;
    } else {
        window.location.href = `${domain}/${pathArr[1]}/${pathArr[2]}/${defaultPage}/${defaultPageSize}/${el.value}`;
    }
}

function confirmDeleteRole() {
    return confirm('Bạn chắc chắn muốn xóa role này không?')
}

function confirmDeleteUser() {
    return confirm('Bạn chắc chắn muốn xóa user này không?')
}

//function select2

$(document).ready(function () {
    $('.js-basic-multiple').select2();
});

//hidden confirm delete user
function confirmDel(el, id) {
    el.setAttribute("hidden", true);
    let confirm = document.getElementById(id);
    confirm.removeAttribute("hidden");
}

function afterConfirmDel(id) {
    let delBtn = document.getElementById(id);
    delBtn.setAttribute("hidden", true);
    let confirm = document.getElementById(`del_${id}`);
    confirm.removeAttribute("hidden");
}