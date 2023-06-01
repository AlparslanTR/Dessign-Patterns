document.getElementById('updateButton').addEventListener('click', function (event) {
  event.preventDefault(); // Formun submit işlemini durdurur

  Swal.fire({
    title: 'Emin misiniz?',
    text: 'Değişiklikleri kaydetmek istediğinizden emin misiniz?',
    icon: 'question',
    showCancelButton: true,
    confirmButtonText: 'Evet',
    cancelButtonText: 'Hayır'
  }).then((result) => {
    if (result.isConfirmed) {
      document.getElementById('myForm').submit(); // Formu submit eder
    }
  });
});
