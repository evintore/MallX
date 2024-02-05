import React, { useState } from "react";
import { Form, Input, Button, Row, Col } from "antd";
import { Card } from "antd";

function Login(props) {
  const [isError, setIsError] = useState(false);

  const onFinish = async (values) => {
    const res = await fetch("api/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(values),
    }).then((response) => response.json());

    if (res.statusCode === 404) {
      setIsError(true);
    }

    if (res.statusCode === 200) {
      props.onLoginSuccess(res.data);
    }
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  return (
    <Form
      name="basic"
      labelCol={{ span: 8 }}
      wrapperCol={{ span: 20 }}
      initialValues={{ remember: true }}
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
      autoComplete="off"
    >
      <Row
        type="flex"
        justify="center"
        align="middle"
        style={{ minHeight: "100vh", flexDirection: "column" }}
      >
        <Card title="" bordered={false}>
          <Col>
            <Form.Item
              label="Email"
              name="email"
              rules={[{ required: true, message: "Please input your email!" }]}
            >
              <Input />
            </Form.Item>
          </Col>

          <Col>
            <Form.Item
              label="Password"
              name="password"
              rules={[
                { required: true, message: "Please input your password!" },
              ]}
            >
              <Input.Password />
            </Form.Item>
          </Col>
          <Col>
            <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
              <Button type="primary" htmlType="submit">
                Login
              </Button>
            </Form.Item>
          </Col>
          <Col>
            <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
              {isError && (
                <p
                  style={{
                    color: "red",
                  }}
                >
                  Invalid username or password!
                </p>
              )}
            </Form.Item>
          </Col>
        </Card>
      </Row>
    </Form>
  );
}

export default Login;
