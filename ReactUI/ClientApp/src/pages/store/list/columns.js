import ActionColumn from "../../../components/dataTable/ActionColumn";

export const columns = [
  {
    title: "AVM",
    dataIndex: "mallInfoName",
    key: "mallInfoName",
    sorter: true,
    render: (value) => <a>{value}</a>,
  },
  {
    title: "Marka",
    dataIndex: "brandName",
    key: "Name",
    sorter: true,
    render: (value) => <a>{value}</a>,
  },
  {
    title: "Mağaza",
    dataIndex: "storeName",
    key: "storeName",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "Kat",
    dataIndex: "floor",
    key: "floor",
    sorter: true,
    render: (text) => <a>{text}</a>,
  },
  {
    title: "İşlem",
    key: "action",
    render: (text, record) => <ActionColumn query="store" id={record.pkId} />,
  },
];
