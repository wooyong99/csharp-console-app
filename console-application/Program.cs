using service_client;
using System.Net.Http;

class Program
{
    static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();
        MemberApi memberApi = new MemberApi(httpClient);
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n회원 관리 시스템");
            Console.WriteLine("1. 회원 등록");
            Console.WriteLine("2. 회원 조회");
            Console.WriteLine("3. 회원 수정");
            Console.WriteLine("4. 회원 삭제");
            Console.WriteLine("5. 전체 회원 조회");
            Console.WriteLine("6. 종료");
            Console.Write("선택: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("LoginId: ");
                    string loginId = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    var signupRequest = new SignupRequest
                    {
                        LoginId = loginId,
                        Password = password,
                        Name = name
                    };
                    var isSuccess = await memberApi.SignupAsync(signupRequest);
                    if (isSuccess)
                    {
                        Console.WriteLine("회원가입 성공");
                    }
                    else
                    {
                        Console.WriteLine("회원가입 실패");
                    }
                    

                    break;

                case "2":
                    Console.Write("조회할 회원 ID: ");
                    int id = int.Parse(Console.ReadLine());
                    var memberInfo = await memberApi.GetMemberByIdAsync(id);
                  
                    if (memberInfo != null)
                    {
                        Console.WriteLine($"회원 정보: {memberInfo.ToString()}");
                    }
                    else
                    {
                        Console.WriteLine("회원 조회 실패: 존재하지 않는 회원입니다.");
                    }
                    break;

                case "3":
                    Console.Write("수정할 회원 ID: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("새로운 Name: ");
                    string newName = Console.ReadLine();

                    var updateRequest = new UpdateNameRequest
                    {
                        Id = updateId,
                        Name = newName
                    };

                    var updateResponse = await memberApi.UpdateMemberNameAsync(updateRequest);

                    if (updateResponse)
                    {
                        Console.WriteLine($"{updateId}번 회원 정보 수정 성공 !");
                    }
                    else
                    {
                        Console.WriteLine("회원 정보 수정 실패 !");
                    }

                    break;

                case "4":
                    Console.Write("삭제할 회원 ID: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    var deleteResponse = await memberApi.DeleteMemberAsync(deleteId);

                    if (deleteResponse)
                    {
                        Console.WriteLine($"{deleteId}번 회원 삭제 성공 !");
                    }
                    else
                    {
                        Console.WriteLine("회원 삭제 실패 !");
                    }
                    break;

                case "5":
                    var memberList = await memberApi.GetMemberAllAsync();
                    foreach (var memberListResponse in memberList)
                    {
                        Console.WriteLine(memberListResponse);
                    }

                    //memberService.GetAllMembers();
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("프로그램을 종료합니다.");
                    break;

                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 시도하세요.");
                    break;
            }
        }
    }
}