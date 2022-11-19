// Navigation Menu Controller Functions
const openMenu = () => {
    $(".global-menu").show();
    $(".global-menu-container").addClass("global-menu-container-show");
}

const closeMenu = () => {
    $(".global-menu-container").removeClass("global-menu-container-show");
    setTimeout(() => { $(".global-menu").hide(); }, 400);
}

// Input Image Preview Controller Functions
const showPreview = (input) => {
    if (input.files && input.files[0]) {
        if (Number(input.files[0].size) / 1024 < 500) {
            $(".image-group-container").hide();
            $(".image-group-container-edit").hide();
            $(".image-group-container-preview").show();
            $(".image-group-container-preview-edit").show();
            $("#error-image").html("");

            var reader = new FileReader();
            reader.onload = function (e) {
                $(".image-group-container-preview").css(`background`, `url(${e.target.result}) no-repeat center center / cover`);
                $(".image-group-container-preview-edit").css(`background`, `url(${e.target.result}) no-repeat center center / cover`);
            }
            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#TourModel0").val(null);
            $("#error-image").html("Tour image size is too large, please select an image less than 500kb.");
        }
    }
    else {
        $("#error-image").html("Tour image is required.");
    }
}

const closePreview = (input) => {
    $(".image-group-container").show();
    $(".image-group-container-edit").show();
    $(".image-group-container-preview").hide();
    $(".image-group-container-preview-edit").hide();
    $("#TourModel0").val(null);
}

// Validations
const validationForName = () => {
    let value = $("#TourModel1").val();
    value = value.trim();

    if (!value) {
        $("#error-name").html("Tour name is required.");
    } else {
        $("#error-name").html("");
    }
}
const validationForAddress = () => {
    let value = $("#TourModel2").val();
    value = value.trim();

    if (!value) {
        $("#error-address").html("Tour address is required.");
    } else {
        $("#error-address").html("");
    }
}
const validationForRating = () => {
    let value = $("#TourModel3").val();
    value = value.trim();

    if (!value) {
        $("#error-rating").html("Tour rating is required.");
    } else if (isNaN(Number(value))) {
        $("#error-rating").html("Tour rating must be a number.");
    } else if (Number(value) < 0 || Number(value) > 5) {
        $("#error-rating").html("Tour rating must be between 0 and 5.");
    } else {
        $("#error-rating").html("");
    }
}
const validationForDescription = () => {
    let value = $("#TourModel4").val();
    value = value.trim();

    if (!value) {
        $("#error-description").html("Tour description is required.");
    } else {
        $("#error-description").html("");
    }
}
const userNameValidationForSignin = () => {
    let value = $("#AccountModel1").val();
    value = value.trim();

    if (!value) {
        $("#error-username-signin").html("Username is required.");
    } else {
        $("#error-username-signin").html("");
    }
}
const userPasswordValidationForSignin = () => {
    let value = $("#AccountModel2").val();

    if (!value) {
        $("#error-password-signin").html("Password is required.");
    } else {
        $("#error-password-signin").html("");
    }
}

// Delete functionality
const deleteModalHandler = (e) => {
    const d = e.target.id.split('-');
    console.log( Number(d[1]) );
}

/////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////
$(document).ready(() => {
    // Necessary variables
    let id;

    // Hide Menu & Add click event listner for menu
    $(".global-menu").hide();
    $(".deleteModal").hide();
    $("#open-menu").click(openMenu);
    $("#close-menu").click(closeMenu);

    // Hide preview and add listner when image field is selected
    $(".image-group-container-preview").hide();
    $(".image-group-container-edit").hide();
    $("#TourModel0").change(function () { showPreview(this); });
    $("#preview-cancel-layer").click(function () { closePreview(this) });

    // Other fields form validation
    $("#TourModel1").blur(validationForName);
    $("#TourModel2").blur(validationForAddress);
    $("#TourModel3").blur(validationForRating);
    $("#TourModel4").change(validationForDescription);
    // validation for signin form
    $("#AccountModel1").blur(userNameValidationForSignin);
    $("#AccountModel2").blur(userPasswordValidationForSignin);
    $(".show-off").click(function () {
        $(".show-off").hide();
        $(".show-on").show();
        $("#AccountModel2").get(0).type = 'text';
    });
    $(".show-on").click(function () {
        $(".show-on").hide();
        $(".show-off").show();
        $("#AccountModel2").get(0).type = 'password';
    });

    // Tour delete handler
    $(".deleteModal").hide();
    $(".tour-delete-button").click((e) => { id = e.target.id.split('-')[1]; $(".deleteModal").show(); });
    $("#tour-delete-cancel-btn").click(() => { $(".deleteModal").hide(); })
    $("#tour-delete-confirm-btn").click(function () {
        $.ajax({
            type: "POST",
            url: `/Tour/Delete/${id}`,
            data: { tourId: id },
            success: (result) => {
                location.reload(true);
                $(".deleteModal").hide();
                location.reload(true);
            }
        });
    });

    // Show only few words of tour desc in tour cart
    $(".home-tour-desc").each(function (i, obj) {
        let str = $(obj).html();
        let arrayOfStr = str.split(' ');

        if (arrayOfStr.length > 35) {
            str = arrayOfStr.slice(0, 35).join(' ') + '....';
            $(obj).html(str);
        }
    });

    // If search field is empty restrict to submit
    $("#search-form").submit(function (e) {
        if ($("#search-form-field").val() == "") {
            e.preventDefault();
        }
    });
});
