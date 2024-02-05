import React from "react";
import { Space, Popconfirm, Button } from "antd";
import { Link } from "react-router-dom";
import { useMutation, useQueryClient } from "react-query";
import api from "../../api";

const ActionColumn = (props) => {
  const { query, id, path } = props;

  const queryClient = useQueryClient();
  const deleteRecord = useMutation(api.delete, {
    onSuccess: () => {
      queryClient.invalidateQueries(query);
    },
  });

  const deleteHandler = () => {
    deleteRecord.mutate({ query, id });
  };

  return (
    <Space size="middle">
      <Link to={`/${path ?? query}/${id}`}>
        <Button>Düzenle</Button>
      </Link>
      <Popconfirm
        placement="top"
        title="Kaydı silmek istiyor musunuz?"
        onConfirm={deleteHandler}
        okText="Evet"
        cancelText="Hayır"
      >
        <Button>Sil</Button>
      </Popconfirm>
    </Space>
  );
};

export default ActionColumn;
