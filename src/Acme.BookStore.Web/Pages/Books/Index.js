$(function () {
    var l = abp.localization.getResource('BookStore');
    /* abp.localization.getResource 获取一个函数,该函数用于使用服务器端定义的相同JSON文件对文本进行本地化.
    通过这种方式你可以与客户端共享本地化值. */

    var dataTable = $('#BooksTable').DataTable(
        /* abp.libs.datatables.normalizeConfiguration是一个辅助方法.
        不是必须的, 但是它通过为缺省的选项提供约定的值来简化Datatables配置. */
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            /* abp.libs.datatables.createAjax是另一个辅助方法,
            用来适配ABP的动态JavaScript API代理和Datatable期望的参数格式 */
            ajax: abp.libs.datatables.createAjax(acme.bookStore.books.book.getList),
            /* acme.bookStore.books.book.getList 是动态JavaScript代理函数(上面已经介绍过了) */
            columnDefs: [
                /* 在 columnDefs 部分的开头添加了一个新列,用于"Actions"下拉按钮. */
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    // abp.auth.isGranted(...) 检查前面定义的权限
                                    visible: abp.auth.isGranted('BookStore.Books.Edit'), //CHECK for the PERMISSION
                                    /* "Edit" 动作简单地调用 editModal.open() 打开编辑模态框. */
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('BookStore.Books.Delete'), //CHECK for the PERMISSION
                                    /* confirmMessage 执行 action 前向用户进行确认 */
                                    confirmMessage: function (data) {
                                        return l('BookDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        acme.bookStore.books.book
                                            .delete(data.record.id)
                                            .then(function () {
                                                /* abp.notify.info 执行删除操作后显示一个通知信息. */
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Author'),
                    data: "authorName"
                },
                {
                    title: l('Type'),
                    data: "type",
                    render: function (data) {
                        return l('Enum:BookType.' + data);
                    }
                },
                {
                    title: l('PublishDate'),
                    data: "publishDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                },
                {
                    title: l('Price'),
                    data: "price"
                },
                {
                    title: l('CreationTime'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );

    /* abp.ModalManager 是一个在客户端管理modal的辅助类.
    它内部使用了Twitter Bootstrap的标准modal组件,但通过简化的API抽象了许多细节. */
    
    // abp.appPath => 默認 Pages的根目錄 所以值為 "/"
    var createModal = new abp.ModalManager(abp.appPath + 'Books/CreateModal');

    /* 增加了一个新的 ModalManager 名为 editModal 打开编辑模态框. */
    var editModal = new abp.ModalManager(abp.appPath + 'Books/EditModal');

    /* createModal.onResult(...) 用于在创建书籍后刷新数据表格. */
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    /* editModal.onResult(...) 当你关闭编程模态框时进行回调刷新数据表格. */
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    /* createModal.open(); 用于打开modal创建新书籍. */
    $('#NewBookButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
