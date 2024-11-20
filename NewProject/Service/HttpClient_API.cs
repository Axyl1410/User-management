using RestSharp;
using System.Text;

namespace NewProject.Service
{
    public   class HttpClient_API
    {
       
        public  async Task<string>GET(string url, string endpoint)
        {
            // Cấu hình RestClient với URL cơ sở
            var options = new RestClientOptions(url)
            {
                MaxTimeout = 10000 // Thời gian chờ tối đa là 10 giây
            };
            var client = new RestClient(options);

            // Tạo yêu cầu GET đến endpoint
            var request = new RestRequest(endpoint, Method.Get);

            // Thực hiện yêu cầu
            RestResponse response = await client.ExecuteAsync(request);

            // Kiểm tra nếu phản hồi thành công
            if (response.IsSuccessful)
            {
                // Trả về nội dung của phản hồi
                return response.Content;
            }
            else
            {
                // Xử lý lỗi, trả về thông báo lỗi với mã trạng thái
                return $"Error: {response.StatusCode} - {response.ErrorMessage}";
            }
        }
        public static async Task<object> httpClientPost(object jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                // URL của API cần gửi POST request
                string url = "http://example.com/api/students";

                // Dữ liệu JSON cần gửi (thay đổi theo yêu cầu của bạn)


                // Chuyển đổi dữ liệu sang JSON string
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);

                // Tạo HttpContent từ JSON string
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Gửi POST request
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Kiểm tra kết quả response
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("POST thành công!");
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // return   Newtonsoft.Json.JsonConvert.SerializeObject(responseBody);
                    return responseBody;
                }
                else
                {
                    return $"POST thất bại. Mã lỗi:" + response.StatusCode;
                }


            }
        }

    }
}
