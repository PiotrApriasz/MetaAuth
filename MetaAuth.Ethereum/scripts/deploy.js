async function main() {
    const MetaAuth = await ethers.getContractFactory("MetaAuth");

    const deploy = await MetaAuth.deploy();
    console.log("Contract deployed to address: ", deploy.address);
}

main()
    .then(() => process.exit(0))
    .catch(error => {
        console.log(error);
        process.exit(1);
    });