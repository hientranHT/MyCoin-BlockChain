using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Model
{

    public static class MyBlockChain
    {
        public static BlockChain blockChain = new BlockChain();
    }

    public class BlockChain
    {
        public IList<Block> Chain { set; get; }
        public int Difficulty { set; get; } = 2;

        public BlockChain()
        {
            InitializeChain();
            AddGenesisBlock();
        }


        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, new Transaction());
        }

        public void AddGenesisBlock()
        {

            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Mine(this.Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }


        public void CopyToBlockBindingList(BindingList<Block> blockList)
        {
            for (int i = 0; i < Chain.Count; i++)
            {
                blockList.Add(Chain[i]);
            }
        }

        public int HowManyMoney(string username)
        {
            int money = 0;
            for (int i = 0; i < Chain.Count; i++)
            {
                if(Chain[i].Data.Sender == username)
                {
                    money -= Chain[i].Data.Amount;
                }

                if (Chain[i].Data.Receiver == username)
                {
                    money += Chain[i].Data.Amount;
                }

                if (Chain[i].Data.Miner == username)
                {
                    money += 10;
                }
            }
            return money;
        }
    }

    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public Transaction Data = new Transaction();
        public int Nonce { get; set; } = 0;
        public string Message { get; set; }

        public Block(DateTime timeStamp, string previousHash, Transaction data)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;

            Data.Sender = data.Sender;
            Data.Receiver = data.Receiver;
            Data.Amount = data.Amount;
            Data.Miner = data.Miner;

            Message ="Sender: " + Data.Sender + "/ Receiver: "+  Data.Receiver + "/ Amount: " + Data.Amount + "/ Miner: " + Data.Miner + " + 10 Coin";

            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}-{Nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
