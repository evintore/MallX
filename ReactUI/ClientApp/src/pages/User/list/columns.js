import ActionColumn from "../../../components/dataTable/ActionColumn";

export const columns = [
  {
    title: "Id",
    dataIndex: "pkId",
    key: "pkId",
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Kullanıcı",
    dataIndex: "fullName",
    key: "fullName",
    render: (text) => <a>{text}</a>,
  },
  {
    title: "E posta",
    dataIndex: "mail",
    key: "mail",
    render: (text) => <a>{text}</a>,
  },
  {
    title: "İşlem",
    key: "action",
    render: (text, record) => <ActionColumn query="user" id={record.pkId} />,
  },
];
