const fs = require("fs");
const solc = require("solc");
const path = require("path");

const contract_names = [
  "NexusFactory",
  "VaultV1Controller",
  "Nexus",
  "VaultV1"
];
const contracts_root = "contracts";

const output_path = "artifacts";

fs.rmSync(output_path, { recursive: true, force: true });

if (!fs.existsSync(output_path)) {
  fs.mkdirSync(output_path);
}

function getAllFiles(dirPath, arrayOfFiles) {
  arrayOfFiles = arrayOfFiles || [];
  files = fs.readdirSync(dirPath);

  files.forEach((file) => {
    try {
      if (fs.statSync(path.join(dirPath, file)).isDirectory()) {
        arrayOfFiles = getAllFiles(path.join(dirPath, file), arrayOfFiles);
      } else {
        if (path.extname(file) != ".sol") {
          return;
        }

        arrayOfFiles.push(path.join(dirPath, file));
      }
    } catch (error) {}
  });

  return arrayOfFiles;
}

const sourceFiles = getAllFiles(contracts_root, null);

var sources = {};

sourceFiles.forEach((file) => {
  sources[path.basename(file)] = fs.readFileSync(file, "utf-8");
});

contract_names.forEach((contract_name) => {
  var input = {
    language: "Solidity",
    sources: { main: { content: sources[contract_name + ".sol"] } },
    settings: {
      outputSelection: {
        "*": {
          "*": ["*"],
        },
      },
    },
  };

  function findImports(relativePath) {
    const source = sources[path.basename(relativePath)];
    return { contents: source };
  }

  var output = JSON.parse(
    solc.compile(JSON.stringify(input), { import: findImports })
  );

  if (output.contracts == null) {
    console.log(output);
    return;
  }

  const compiledContract = output.contracts["main"][contract_name];

  let data = JSON.stringify(compiledContract.abi);
  const abiOutFile = path.resolve(output_path, contract_name + ".abi");

  fs.writeFileSync(abiOutFile, data, "utf8");
});
