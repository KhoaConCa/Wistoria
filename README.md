<h1 align="center">Trường Đại học Tư thục Quốc Tế Sài Gòn<br/>
    Tất cả những điều nên biết trước khi tham gia dự án và tham khảo dự án
</h1>

# [**Table Of Content**](#table-of-content)
- [**Table Of Content**](#table-of-content)
- [**Introduction**](#introduction)
- [**Folder organization**](#folder-organization)
- [**Create branch and code**](#create-branch-and-code)
  - [**Abbreviated Dev name**](#abbreviated-dev-name)
  - [**Branch**](#branch-)
  - [**Code**](#code-)
    - [**XML documentation comment**](#xml-documentation-comment)
    - [**Directive**](#directive-)
    - [**SOLID**](#solid-)
- [**Post Script**](#post-script)
- [**Move or get zapped 💙**](#move-or-get-zapped)

# [**Introduction**](#introduction)

Xin kính chào, xin mến chào và xin thân thương chào tất cả các bạn. 

Mình tên là **`Dương Nhật Khoa`** và nhóm của mình gồm có bốn thành viên bao gồm: **`Trần Ngân Bảo`**, **`Lưu Chiếu Dũng`**, **`mình`** và **`Nguyễn Ngọc Phú`**. 
Chúc các bạn ngày mới thật là `mạnh giỏi`. Vì một ngày mà chúng ta sống khỏe `mạnh` là đã được 50% công lực. Nhưng tồn tại thì vẫn khỏe mạnh được nên để gọi là sống thì trí tuệ của bản thân phải phát triển, phải hữu dụng, 
phải làm được cái này cái kia. Đặc biệt phải sử dụng thời gian của mình thực sự hiệu quả thì đó là cái `giỏi`. Một ngày trôi qua mà khi nhìn lại bạn tự thấy mình có **khỏe** và có **giỏi** thì chúc mừng bạn đã có được 
một **`ước mơ của đời người`**, không cần gì quá cao xa.

Đồ án này được viết bằng ngôn ngữ **`C#`** trên **`Visual Studio`** và dùng Engine **`Unity`**.

Như tiêu đề bên trên, reponsitory này sẽ nói về dự án của nhóm mình làm trong năm học 2024 này.

Let's go! 🔥🔥🔥

# [**Folder organization**](#folder-organization)

Chúng mình sẽ sử dụng mô hình và tổ chức files, folders dựa trên mô hình MVC, trong đó bao gồm:

1. **`Command`**: Gửi thông tin cần thiết đến **`Handler`** để thực hiện một hành động cụ thể.
2. **`Data`**: Là thông tin mà **`Command`** cần để thực hiện hành động.
3. **`Handler`**: Sử dụng **`Data`** để xử lý lệnh và quyết định cách thức thực hiện nó. Cũng có thể cập nhật hoặc tương tác với **`View`** để phản ánh sự thay đổi trạng thái sau khi lệnh được thực hiện.
4. **`View`**: Sẽ cung cấp cho người dùng giao diện để tương tác, có thể gửi lệnh đến **`Handler`** khi người dùng thực hiện một hành động.

# [**Create branch and code**](#create-branch-and-code)

Trong phần này chúng ta sẽ đi vào quy tắc tạo branch và các tổ chức code của team mình nhé.

## [**Abbreviated Dev name**](#abbreviated-dev-name)

Đầu tiên sẽ là quy tắt đặt tên cho từng thành viên. Chúng ta sẽ giữ lại tên và viết tắt họ và tên đệm của bản thân, ví dụ: **`Dương Nhật Khoa`** -> **`KhoaDN`**.

## [**Branch**](#branch-)

Cách tạo branch sẽ là các tiền tố được tách từ các branch cụ thể. Chúng mình có 3 branch chính, đó là:

1. **`Main`**: Là branch chứa code hoàn thiện của dự án.
2. **`Dev`**: Hay còn gọi là Develop, là branch dùng để phát triển dự án và tích hợp các tính năng.
3. **`Pro`**: Hay còn gọi là Production, là branch chứa code có thể triển khai trực tiếp trên môi trường sản xuất và có thể kết hợp với **`main`**.

Từ những branch đó chúng mình sẽ có những tiền tố sau:

1. Tiền tố **`bug/<id-trello>-<tên-dev>`** sẽ tách từ branch **`Dev`** để sửa lỗi.
2. Tiền tố **`feature/<id-trello>-<tên-dev>`** sẽ tách từ branch **`Dev`** để phát triển tính năng mới.
3. Tiền tố **`hotfix/<id-trello>-<tên-dev>`** sẽ tách từ branch **`Pro`** để khắc phục sự cố gấp.
4. Tiền tố **`refactor/<id-trello>-<tên-dev>`** sẽ tách từ branch **`Dev`** để tái cấu trúc code.
5. Tiền tố **`docs/<id-trello>-<tên-dev>`** sẽ tách từ branch **`Dev`** để chỉnh sửa/thêm tài liệu.
6. Tiền tố **`perf/<id-trello>-<tên-dev>`** sẽ tách từ branch **`Dev`** để tối ưu hiệu suất.

## [**Code**](#code-)

### [**XML documentation comment**](#xml-documentation-comment)

Chúng mình có sử dụng XML Documentation comment trong C# để mô ta chức năng của từng Method hoặc các thành phần trong code. Điều này giúp chúng mình hiểu code của nhau hơn trong quá trình làm việc chung.
Sau đây là ví dụ:

    /// <summary>
    /// Caculate damage for Enemy
    /// </summary>
    /// <param name="dmg">Damage Caused</param>
    public void TakeDamge(int dmg)
    {

    }

Chỉ cần nhấn /// ở bên trên của một Method bất kỳ thì đoạn mã này sẽ xuất hiện và cho phép bạn mô tả tác dụng của Method đó trong **`<summary></sumary>`**. Ngoài ra còn có thể giải thích từng tham số của 
Method đó.

### [**Directive**](#directive)

Đương nhiên phần không thể thiếu chính là Directive:

    #region -- Fields --
    
    private AttackAnim attackAnimation;
    private CharacterStatus status;
    public Transform attackPoint;
    public float attackRange = 0.9f;
    public LayerMask enemyLayers;
    
    #endregion

Việc sử dụng Directive **`#region`** giúp tụi mình quản lý được tốt hơn về mặt các chức năng nào đang được code; phân vùng được Implements, Overrides, Methods, Fields và Properties với nhau. Quy tắc chung 
chính là đặt theo thứ tự:

1. **`-- Implements --`**
2. **`-- Overrides --`**
3. **`-- Methods --`**
4. **`-- Fields --`**
5. **`-- Properties --`**

Ngoài ra sẽ còn có những Directive nhằm mục đích gom nhóm chức năng sẽ được **`viết liền trước Method đầu tiên`** của chức năng đó. Ví dụ:

    public void Attack()
    {
        #region -- Animations --
        if (attackAnimation != null)
        {
            attackAnimation.PlayAnimation();
            StartCoroutine(PerformAttack());
        }
        else
        {
            Debug.LogWarning("AttackAnim component is not assigned.");
        }
        #endregion
    }

# [**SOLID**](#solid-)

Chúng mình cố hết sức để đảm bảo tính chất hướng đối tượng của OOP và nếu có gì sai sót mong các bạn bỏ qua cho. Dưới đây là năm quy tắc của OOP:

1. **`Single Responsibility Principle`**: Một class chỉ nên thực hiện một trách nhiệm duy nhất (chỉ có một lý do để thay đổi).
2. **`Open/Closed Principle`**: Có thể thoải mái mở rộng class, nhưng không được sửa đổi bên trong nó và dễ dàng để bảo trì.
3. **`Liskov Substitution Principle`**: Các object của class con có thể thay thế class cha mà không làm thay đổi tính đúng đắn.
4. **`Interface Segregation Principle`**: Nên chia nhỏ Interface ra làm những việc cụ thể.
5. **`Dependency Inversion Principle`**: Cố gắng giao tiếp với nhau bằng abstract hoặc interface, đừng bao giờ giao tiếp qua class con đã được kế thừa.

# [**Post Script**](#post-script)

Mình xin thay mặt cả team gửi những lời cảm ơn sâu sắc nhất đến những bạn đã đọc đến cuối cùng. Điều đó làm cho team mình thấy được sự kiên trì, tìm tòi học hỏi của các bạn là rất lớn. 
Chúng mình chỉ chia sẻ những thứ gì mà bản thân của mình đã làm được đến cho các bạn. Nếu các bạn thấy hữu ích, hãy chia sẽ với bạn bè các bạn để có thể giúp đỡ họ và tặng cho họ bài 
học này.

Một lần nữa, cảm ơn các bạn lắng nghe tụi mình và chúc các bạn gặp được nhiều thành công trong cuộc sống.💙💚💛

**Trân trọng,**
Dương Nhật Khoa

*25 tháng 9 năm 2024*

# [**Move or get zapped 💙**](#move-or-get-zapped)
