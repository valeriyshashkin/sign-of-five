import Head from "next/head";

export default function Home() {
  return (
    <>
      <Head>
        <title>Змейка</title>
        <meta name="description" content="Змейка" />
      </Head>
      <h1>Змейка</h1>
      <div className="center">
        <a href="snake-installer.exe">Скачать</a>
      </div>
      <style jsx>{`
        .center {
          display: flex;
          flex-direction: column;
          justify-content: center;
        }

        h1 {
          text-align: center;
          margin-top: 200px;
        }

        a {
          background: blue;
          color: white;
          text-decoration: none;
          padding: 15px;
          font-size: 20px;
          margin: 0 auto;
          display: inline-block;
        }
      `}</style>
    </>
  );
}
