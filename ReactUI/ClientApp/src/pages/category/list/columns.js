import ActionColumn from "../../../components/dataTable/ActionColumn";

export const columns = [
  {
    title: "Kategori",
    dataIndex: "categoryName",
    key: "categoryName",
    sorter: true,
    render: (value) => <a>{value}</a>,
  },
  {
    title: "Alt kategori sayısı",
    dataIndex: "subcategoryCount",
    key: "subcategoryCount",
    //sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "İşlem",
    key: "action",
    render: (text, record) => (
      <ActionColumn query="category" id={record.pkId} />
    ),
  },
];
