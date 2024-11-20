document.getElementById('logout').addEventListener('click', function (event) {
    event.preventDefault(); // Ngăn chặn hành động mặc định của liên kết

    // Thực hiện yêu cầu GET đến /logout
    fetch('/logout', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'same-origin' // Gửi kèm thông tin xác thực như cookie nếu cần
    })
        .then(response => response.json()) // Chuyển đổi phản hồi từ server sang JSON
        .then(data => {
            // Kiểm tra phản hồi từ server
            if (data.mes === "22") {
                Swal.fire({
                    icon: 'success',
                    title: 'Logout',
                    text:"Logout ok", // Hiển thị thông báo từ server, ví dụ: "logout ok"
                    timer: 2000, // Thông báo tự động ẩn sau 2 giây
                    showConfirmButton: false
                });

              //  alert(data.data); // Hiển thị thông báo từ server, ví dụ: "logout ok"
                window.location.href = '/pagelg'; // Chuyển hướng sau khi logout thành công
            } else {
                Swal.fire({
                    icon: 'eror',
                    title: 'Logout',
                    text: "Logout failed", // Hiển thị thông báo từ server, ví dụ: "logout ok"
                    timer: 2000, // Thông báo tự động ẩn sau 2 giây
                    showConfirmButton: false
                });
                
                
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
});