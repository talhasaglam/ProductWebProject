
    function ShowConfirm() {
        var confirmation = confirm("Emin misiniz?");
        if (confirmation) {
          alert("Kayıt Silinmiştir.");
        }
        return confirmation;
    };
