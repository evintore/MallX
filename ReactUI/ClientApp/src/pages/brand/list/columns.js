import ActionColumn from "../../../components/dataTable/ActionColumn";

export const columns = [
  {
    title: "Marka",
    dataIndex: "brandName",
    key: "brandName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Kategori",
    dataIndex: "category",
    key: "category",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Alt Kategori",
    dataIndex: "subCategory",
    key: "subCategory",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "İşlem",
    key: "action",
    render: (text, record) => <ActionColumn query="brand" id={record.pkId} />,
  },
];
